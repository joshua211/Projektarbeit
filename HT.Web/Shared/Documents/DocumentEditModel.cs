using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HT.Web.Shared.Documents
{
    public class DocumentEditModel
    {
        public DocumentEditModel()
        {
            Roles = new List<string>() { Core.Shared.Roles.User.Name };
            IsVisible = true;
        }

        [Required(ErrorMessage = "Das Dokument muss einer Kategorie zugeordnet werden")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Das Dokument benötigt einen Titel")]
        public string Title { get; set; }

        public bool IsVisible { get; set; }
        public List<string> Roles { get; set; }
        public string Content { get; set; }
    }
}