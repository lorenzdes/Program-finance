using System;

public class Finance
{
    // Constructor
    public Finance(int id, string stock, float price)
    {
        ID = id;
        Stock = stock;
        Price = price;
    }

    // Properties
    public int ID { get; set; }
    public string Stock { get; set; }
    public float Price { get; set; }
}
