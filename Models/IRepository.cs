using System.Dynamic;

namespace Mtg
{
    using System;
    using Nancy;
    using Mtg.Model;

    public interface IRepository
    {
        Card[] GetCards (dynamic query);

        Card[] GetCardsBySet (string setId);

        Card GetCard (int id);

        CardSet[] GetSets ();

        CardSet GetSet (string id);
    }
}

