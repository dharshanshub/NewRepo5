namespace Medicure_Entity
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Required_Qty { get; set; }
        public int Instock_Qty { get; set; }
        public float Price { get; set; }
        public int Supplier_Id { get; set; }
        public string Expiry_Date { get; set; }
    }   
}
