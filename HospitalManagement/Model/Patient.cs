namespace HospitalManagement.Model
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string Disease { get; set; } = null!;

        // FIXED: Phone should be string
        public string Phone { get; set; } = null!;
    }
}
