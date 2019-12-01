using System.Collections.Generic;
using System.Linq;

namespace UnitedPigeonAirlines.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Pigeon Pigeon, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Pigeon.PigeonId == Pigeon.PigeonId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Pigeon = Pigeon,
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
            lineCollection.RemoveAll(l => l.Pigeon.PigeonId == Pigeon.PigeonId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Pigeon.BasicPrice * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Pigeon Pigeon { get; set; }
        public int Quantity { get; set; }
    }
}