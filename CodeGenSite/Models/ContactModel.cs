using System.ComponentModel.DataAnnotations;

namespace CodeGenSite.Models
{
    public class ContactModel
    {
        private const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "A Valid Email Required is required.")]
        [RegularExpression(MatchEmailPattern, ErrorMessage = "Valid Email Required is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a message.")]
        public string Description { get; set; }

        /// <summary>
        /// 1 = not ready
        /// 2 = successful
        /// 3 = failed
        /// </summary>
        public int SentSuccesfully { get; set; }
    }
}