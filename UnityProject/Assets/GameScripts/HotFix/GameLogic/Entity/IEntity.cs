using System.Collections.Generic;

namespace GameLogic
{
    public interface IEntity
    {
    }

    public interface IAttribute
    {
        public AttributeDictionary _AttributeDict { get; set; }
    }
}