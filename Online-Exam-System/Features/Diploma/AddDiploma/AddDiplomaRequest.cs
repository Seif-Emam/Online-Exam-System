namespace Online_Exam_System.Features.Diploma.AddDiploma
{
    public class AddDiplomaRequest
    {
        public  string Title { set; get; }
        public  string Description { set; get; }
        public  IFormFile PictureUrl { set; get; }


    }
}
