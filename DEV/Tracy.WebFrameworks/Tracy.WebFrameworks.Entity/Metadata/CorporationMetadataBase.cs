
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
public class CorporationMetadataBase
{


	/// <summary>
    /// 
	/// </summary>
    [Key]
    [Required]
    [Digits]
    [Max(int.MaxValue)]
    [DataMember]
    public object CorporationID { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [Required]
    [Digits]
    [Max(int.MaxValue)]
    [DataMember]
    public object ParentCorpID { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [StringLength(30)]
    [DataMember]
    public object CorporationCode { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [Required]
    [StringLength(50)]
    [DataMember]
    public object CorporationName { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [Required]
    [StringLength(100)]
    [DataMember]
    public object Address { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [StringLength(30)]
    [DataMember]
    public object CreatedBy { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [DataType(DataType.Date)]
    [UIHint("DatePicker")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    [DataMember]
    public object CreatedTime { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [StringLength(30)]
    [DataMember]
    public object LastUpdatedBy { get; set; }


	/// <summary>
    /// 
	/// </summary>
    [DataType(DataType.Date)]
    [UIHint("DatePicker")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    [DataMember]
    public object LastUpdatedTime { get; set; }

}


}
