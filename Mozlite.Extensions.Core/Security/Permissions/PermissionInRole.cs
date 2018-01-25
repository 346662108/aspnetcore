using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mozlite.Extensions.Security.Permissions
{
    /// <summary>
    /// ��ɫȨ�ޡ�
    /// </summary>
    [Table("core_Permissions_In_Roles")]
    public class PermissionInRole
    {
        /// <summary>
        /// ��ɫId��
        /// </summary>
        [Key]
        public int RoleId { get; set; }

        /// <summary>
        /// Ȩ��Id��
        /// </summary>
        [Key]
        public int PermissionId { get; set; }

        /// <summary>
        /// ��ǰȨ���趨��
        /// </summary>
        public PermissionValue Value { get; set; }
    }
}