//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tracy.WebFrameworks.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoleFunction
    {
        public int RoleFunctionID { get; set; }
        public int RoleID { get; set; }
        public int MenuID { get; set; }
        public int FunctionID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual Function Function { get; set; }
    }
}