namespace MiniTrade.ConsoleApp.Models.Game;

public enum PokerCardSuit
{
    Spade = 1,
    Heart = 2,
    Club = 3,
    Diamond = 4
}

public enum PokerCardRank
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
}

public record PokerCard : ICard
{
    public uint Id { get; init; }

    public PokerCardSuit Suit { get; init; }

    public PokerCardRank Rank { get; init; }
}

public interface ICardDeck<T>
{
    IList<T> Cards { get; set; }

    void Shuffle();

    void PrintCards();

    IList<T> GetTop(int count);
}

public record PokerCardDeck : ICardDeck<PokerCard>
{
    public IList<PokerCard> Cards { get; set; }

    public PokerCardDeck()
    {
        int id = 1;

        Cards = new List<PokerCard>();

        foreach (PokerCardSuit suit in Enum.GetValues<PokerCardSuit>())
            foreach (PokerCardRank rank in Enum.GetValues<PokerCardRank>())
                Cards.Add(new PokerCard
                {
                    Id = (uint)id++,
                    Suit = suit,
                    Rank = rank
                });
    }

    public void PrintCards()
    {
        //Console.WriteLine("PRINT CARDS");
        foreach (var card in Cards)
        {
            Console.WriteLine(card);
        }
        //for (int i = 0; i < Cards.Count; i++)
        //{
        //    Console.WriteLine(Cards[i]);
        //}
    }

    public void Shuffle()
    {
        //IList<PokerCard> left = new List<PokerCard>();
        //IList<PokerCard> right = new List<PokerCard>();

        //int i = 0;
        //int half = Cards.Count / 2;
        //for (i = 0; i < half; i++)
        //{
        //    var card = Cards[0];
        //    left.Add(card);
        //    Cards.Remove(card);
        //}



        //Console.WriteLine("LEFT");
        //foreach (var card in left)
        //{
        //    Console.WriteLine(card);
        //}


        //Console.WriteLine("CARDS");
        //foreach (var card in Cards)
        //{
        //    Console.WriteLine(card);
        //}

        //RiffleShuffle();
        RandomShuffle();
        //UniqueCardDeck();
        //RandomShuffle();
        //RandomShuffle();


        PrintCards();

        IList<PokerCard> p1 = new List<PokerCard>();
        IList<PokerCard> p2 = new List<PokerCard>();

        for (int i = 0; i < 5; i++)
        {
            p1.Add(Cards[0]);
            Cards.Remove(Cards[0]);

            p2.Add(Cards[0]);
            Cards.Remove(Cards[0]);
        }

        Console.WriteLine("HAND-p1");
        foreach (var item in p1)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("HAND-p2");
        foreach (var item in p2)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("Card count: {0}", Cards.Count);
    }

    


    public IList<PokerCard> GetTop(int count)
    {
        IList<PokerCard> left = new List<PokerCard>();

        for (var index = 0; index < count; index++)
        {
            var card = Cards[0];
            left.Add(card);
            Cards.Remove(card);
        }

        return left;
    }

    private void RandomShuffle()
    {
        //int i = 0;

        IList<PokerCard> newPile = new List<PokerCard>();
        //int count = Cards.Count;

        //for (i = 0; i < count; i++)
        //{
        //    var idx = Random.Shared.Next(0, Cards.Count - 1);
        //    Cards.Remove(Cards[idx]);
        //    newPile.Add(Cards[idx]);
        //}

        while (Cards.Count > 1)
        {
            var idx = Random.Shared.Next(0, Cards.Count);
            newPile.Add(Cards[idx]);
            Cards.Remove(Cards[idx]);

        }
        newPile.Add(Cards[0]);
        Cards = newPile;
    }

    private void RiffleShuffle()
    {
        IList<PokerCard> leftHalf = GetTop(Cards.Count / 2);
        IList<PokerCard> rightHalf;
        int count;

        if (leftHalf.Count > Cards.Count)
        {
            count = leftHalf.Count;
            rightHalf = Cards;
        }
        else
        {
            count = Cards.Count;
            rightHalf = leftHalf;
            leftHalf = Cards;
        }

        Cards = new List<PokerCard>(count);

        for (var i = 0; i < count - 1; i++)
        {
            Cards.Add(leftHalf[0]);
            leftHalf.Remove(leftHalf[0]);

            Cards.Add(rightHalf[0]);
            rightHalf.Remove(rightHalf[0]);
        }

        Cards.Add(leftHalf[0]);
        leftHalf.Remove(leftHalf[0]);

        if (rightHalf.Count > 0)
        {
            Cards.Add(rightHalf[0]);
            rightHalf.Remove(rightHalf[0]);
        }


        //int count = 0;

        //int i = 0;
        //int half = Cards.Count / 2;

        //for (i = 0; i < half; i++)
        //{
        //    var card = Cards[0];
        //    left.Add(card);
        //    Cards.Remove(card);
        //}






        //right = Cards;

        //Console.WriteLine("Left {0}, Right {1}", 
        //    left.Count, right.Count);

        //Cards = new List<PokerCard>();
        //int count = left.Count;

        //for (i = 0; i < count; i++)
        //{

        //    Cards.Add(left[0]);
        //    left.Remove(left[0]);
        //    Cards.Add(right[0]);
        //    right.Remove(right[0]);
        //}


    }

    private void UniqueCardDeck()
    {
        HashSet<int> id = new HashSet<int>();
        foreach (var item in Cards)
        {
            if (!id.Add((int)item.Id))
                throw new Exception();
        }
    }

}
