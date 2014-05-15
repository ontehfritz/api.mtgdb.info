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
        Task<Card> GetRandomCard ();
        Task<Card> GetRandomCardInSet(string setId);

        //Write methods for open db functions
        Task<Card> UpdateCardField<T> (int mvid, string field, T value);
        Task<Card> UpdateCardRulings (int mvid, Ruling[] rulings);
        Task<Card> UpdateCardFormats (int mvid, Format[] formats);
        Task<Card> AddCard(Card newCard);
        Task<CardSet> AddCardSet(CardSet newSet);
    }
}

