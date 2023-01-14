namespace Core.Models
{
    public class Cities
    {
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }

        public string completed
        {
            get { 
                return $"{city}, {state}, {country}";
            }
        }
    }
}
