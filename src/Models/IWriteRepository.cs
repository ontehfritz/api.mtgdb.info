using System;
using System.Threading.Tasks;
using Mtg.Model;

namespace Mtg
{
    public interface IWriteRepository
    {
        //Write methods for open db functions
        Task<Card> UpdateCardField<T> (int mvid, string field, T value);
        Task<Card> UpdateCardRulings (int mvid, Ruling[] rulings);
        Task<Card> UpdateCardFormats (int mvid, Format[] formats);
        Task<Card> AddCard(Card newCard);
        Task<CardSet> AddCardSet(CardSet newSet);
        Task<CardSet> UpdateCardSet<T>(string id, string field, T value);
    }
}

