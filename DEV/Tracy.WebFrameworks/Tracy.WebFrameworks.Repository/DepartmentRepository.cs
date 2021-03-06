﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracy.WebFrameworks.IRepository;
using Tracy.WebFrameworks.Entity;
using Tracy.WebFrameworks.Common.Helper;
using Tracy.WebFrameworks.Data;
using System.Linq.Expressions;
using Tracy.Frameworks.Common.Result;
using Tracy.WebFrameworks.Entity.ViewModel;
using Tracy.Frameworks.Common.Extends;

namespace Tracy.WebFrameworks.Repository
{
    /// <summary>
    /// 部门仓储接口实现
    /// </summary>
    public class DepartmentRepository : IDepartmentRepository
    {
        /// <summary>
        /// 依据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Department GetById(int id)
        {
            Department result = null;
            DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new WebFrameworksDB())
                {
                    result = db.Department.FirstOrDefault(p => p.Id == id);
                }
            });
            return result;
        }

        /// <summary>
        /// 依条件表达式查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IEnumerable<Department> GetByCondition(Expression<Func<Department, bool>> filter = null, Func<IQueryable<Department>, IOrderedQueryable<Department>> orderby = null)
        {
            IEnumerable<Department> result = null;
            DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new WebFrameworksDB())
                {
                    var query = db.Department.AsQueryable();
                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }
                    if (orderby != null)
                    {
                        result = orderby(query).ToList();
                    }
                    else
                    {
                        result = query.ToList();
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Department Insert(Department item)
        {
            //CRUD Operation in Connected mode
            using (var db = new WebFrameworksDB())
            {
                var result = db.Department.Add(item);
                if (db.SaveChanges() > 0)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(Department item)
        {
            //CRUD Operation in Connected mode
            using (var db = new WebFrameworksDB())
            {
                var department = db.Department.FirstOrDefault(p => p.Id == item.Id);
                if (department != null)
                {
                    department.Name = item.Name;
                    department.Sort = item.Sort;
                    department.LastUpdatedBy = item.LastUpdatedBy;
                    department.LastUpdatedTime = item.LastUpdatedTime;
                }
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            //CRUD Operation in Connected mode
            using (var db = new WebFrameworksDB())
            {
                var department = db.Department.FirstOrDefault(p => p.Id == id);
                if (department != null)
                {
                    db.Department.Remove(department);
                }

                if (db.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取指定部门下的所有用户(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<User> GetUserByDepartment(GetUserByDepartmentRQ request)
        {
            var result = new PagingResult<User>();
            var departmentIds = request.DeptIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToInt()).ToList();

            DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new WebFrameworksDB())
                {
                    var query = db.User.GroupJoin(db.UserDepartment, user => user.Id, userDepart => userDepart.UserId, (user, userDepart) => new { user, userDepart = userDepart.FirstOrDefault() })
                                .GroupJoin(db.Department, ud => ud.userDepart.DepartmentId, depart => depart.Id, (ud, depart) => new { ud, depart = depart.FirstOrDefault() })
                                .Where(p => departmentIds.Contains(p.depart.Id))
                                .Select(p => p.ud.user);
                    result = query.OrderByDescending(p => p.CreatedTime)
                        .Paging(request.PageIndex, request.PageSize);
                }
            });

            return result;
        }

        /// <summary>
        /// 获取指定公司下的所有部门
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<Department> GetDepartmentByCorp(GetDepartmentByCorpRQ request)
        {
            var result = new List<Department>();
            DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new WebFrameworksDB())
                {
                    var query = db.Department.GroupJoin(db.Corporation, department => department.CorporationId, corp => corp.Id, (department, corp) => new { department, corp = corp.FirstOrDefault() });
                    if (request.CorporationId > 0)
                    {
                        query = query.Where(p => p.department.CorporationId == request.CorporationId);
                    }

                    //排序
                    query = query.OrderBy(p => p.department.CorporationId).ThenBy(p => p.department.Code);

                    result = query.ToList().ConvertAll(p => new Department 
                    {
                        Id = p.department.Id,
                        ParentId= p.department.ParentId,
                        Name= p.department.Name,
                        Code = p.department.Code,
                        Enabled = p.department.Enabled,
                        Sort = p.department.Sort,
                        CreatedTime = p.department.CreatedTime,
                        CorporationId = p.corp.Id,
                        CorporationName = p.corp.Name
                    });
                }
            });

            return result;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteDepartment(DeleteDepartmentRQ request)
        { 
            //删除部门包括子部门
            //解除部门与用户的关系
            var deleteDeptIds = request.DeleteDeptIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.ToInt()).ToList();
            using (var db = new WebFrameworksDB())
            {
                var deleteDepts = db.Department.Where(p => deleteDeptIds.Contains(p.Id)).ToList();
                if (deleteDepts.HasValue())
                {
                    db.Department.RemoveRange(deleteDepts);
                }

                var deleteUserDepts = db.UserDepartment.Where(p => deleteDeptIds.Contains(p.DepartmentId)).ToList();
                if (deleteUserDepts.HasValue())
                {
                    db.UserDepartment.RemoveRange(deleteUserDepts);
                }

                //事务提交
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

    }
}
