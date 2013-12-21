using System.Dynamic;
using System.Threading.Tasks;

namespace Mtg
{
    using System;
    using Nancy;
    using Mtg.Model;

    public interface IRepository
    {
        Task<Card[]> GetCards (dynamic query);
        Task<Card[]> GetCardsBySet (string setId);
        Task<Card> GetCard (int id);
        Task<Card[]> GetCards (string name);
        Task<CardSet[]> GetSets ();
        Task<CardSet> GetSet (string id);
    }
}

