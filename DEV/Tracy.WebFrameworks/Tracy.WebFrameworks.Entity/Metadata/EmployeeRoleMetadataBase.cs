
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DataAnnotationsExtensions;//http://dataannotationsextensions.org/


namespace Tracy.WebFrameworks.Entity.Metadata
{

/// <summary>
///  CustomMetadata基類
/// </summary>
[DataContract]
public class EmployeeRoleMetadataBase
{


	/// <summary>
    /// 
	/// </summary>
    [Key]
    [Required]
    [Digits]
    [Max(int.MaxValue)]
    [DataMember]
    public object ID { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [Required]
    [Digits]
    [Max(int.MaxValue)]
    [DataMember]
    public object EmployeeID { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [Required]
    [Digits]
    [Max(int.MaxValue)]
    [DataMember]
    public object RoleID { get; set; }

}


}
