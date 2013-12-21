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
        Task<Card[]> GetCardsBySet (string setId, int start = 0, int end = 0);
        Task<Card> GetCard (int id);
        Task<Card[]> GetCards (string name);
        Task<CardSet[]> GetSets ();
        Task<CardSet> GetSet (string id);
    }
}

