using System.Collections.Generic;

namespace GameLogic
{
    public interface IEntity
    {
    }

    public interface IAttribute
    {
        public AttributeDictionary AttributeDict { get; set; }
    }
}