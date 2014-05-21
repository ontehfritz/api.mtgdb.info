using System.Dynamic;
using System.Threading.Tasks;
using System;
using Nancy;
using Mtg.Model;

namespace Mtg
{
    public interface IRepository
    {
        Task<Card[]> GetCards (dynamic query);
        Task<Card[]> GetCardsBySet (string setId, int start = 0, int end = 0);
        Task<Card> GetCardBySetNumber (string setId, int setNumber);
        Task<Card> GetCard (int id);
        Task<Card[]> GetCards (string name);
        Task<Card[]> GetCards (int [] multiversIds);
        Task<CardSet[]> GetSets ();
        Task<CardSet[]> GetSets (string [] setIds);
        Task<CardSet> GetSet (string id);
        Task<Card[]> Search (string text, int start = 0, int limit = 0, bool isComplex = false);
        Task<long> SearchTotal (string text, bool isComplex = false);
        Task<Card> GetRandomCard ();
        Task<Card> GetRandomCardInSet(string setId);
    }
}

