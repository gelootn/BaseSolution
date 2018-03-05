namespace BaselineSolution.Bo.Internal
{
    public class DisplayObject
    {

        public DisplayObject()
        {
            
        }

        public DisplayObject(int id, string display)
        {
            Id = id;
            Display = display;
        }

        public int Id { get; set; }
        public string Display { get; set; }
    }
}
