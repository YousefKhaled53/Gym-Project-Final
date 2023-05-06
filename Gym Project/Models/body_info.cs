namespace Gym_Project.Models
{
    public class body_info
    {
        public string username { get; set; }    
        public int weight { get; set; }
        public int height { get; set; }
        public int muscle_mass { get; set; }
        public int fat_percent { get; set; }
        public body_info(string un,int w,int h, int mm, int fp)
        {
            username= un;
            weight= w;
            height= h;    
            muscle_mass= mm;
            fat_percent= fp;    

        }

    }
}
