using System.ComponentModel.DataAnnotations;

namespace TODOAPI.ViewModels
{
    public class CreateTodoVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
