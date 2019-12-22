using System.Collections.Generic;
using System.Linq;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;


namespace UnitedPigeonAirlines.Data.Entities.CartAggregate
{
    public class Cart
    {

        public List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Pigeon Pigeon, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.PigeonId == Pigeon.PigeonId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    PigeonId = Pigeon.PigeonId,
                    Quantity = quantity
                });
               
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Pigeon Pigeon)
        {
            lineCollection.RemoveAll(l => l.PigeonId == Pigeon.PigeonId);
        }

        //public int QuantityReturner(Pigeon pigeon)
        //{


        //    return lineCollection.FirstOrDefault(x=>x.PigeonId==(pigeon.PigeonId-1)).Quantity;

        //}
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
        public void FillOrder(Order order)
        {

            order.Pigeons = new List<PigeonInOrder>();
            foreach (var line in lineCollection)
            {
                
                order.Pigeons.Add(new PigeonInOrder()
                {
                    PigeonId = line.PigeonId,
                    //OrderId = order.OrderId,
                    Quantity = line.Quantity,
                });
            }
            
                

            
        }
    }


}