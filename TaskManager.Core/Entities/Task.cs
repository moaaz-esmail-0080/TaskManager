using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities
{
    public class Task
    {
        [Key] // تعيين Id كمفتاح رئيسي
        public int Id { get; set; }

        [Required] // العنوان مطلوب
        [MaxLength(200)] // الحد الأقصى 200 حرف
        public string Title { get; set; }

        [MaxLength(1000)] // الحد الأقصى 1000 حرف
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false; // افتراضيًا المهمة غير مكتملة

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // تاريخ الإنشاء

        public DateTime? UpdatedAt { get; set; } // تاريخ التحديث (يمكن أن يكون NULL)
    }

}
