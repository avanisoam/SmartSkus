using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SmartSkus.Shared.Enums;

namespace SmartSkus.Api.Models
{
    public class Settings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public DataFormat SelectedBackupFormat { get; set; }
        public int Size { get; set; }
        public string Culture { get; set; }
        public string Theme { get; set; }
        public string Background { get; set; }
        //public bool ShowAllGoals { get; set; }
        //public bool ShowAllTasks { get; set; }
        //public Sort Sort { get; set; }
        public Screen Screen { get; set; }
        //public long ElapsedToDesiredRatioMin { get; set; }
        //public bool ShowElapsedToDesiredRatioOverMin { get; set; }
        //public bool HideEmptyGoals { get; set; }
        //public bool HideGoalsWithSimpleText { get; set; }
        //public bool ShowCategoriesInGoalList { get; set; }
        //public bool HideCompletedTasks { get; set; }
    }
}
