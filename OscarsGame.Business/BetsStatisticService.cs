using OscarsGame.Business.Interfaces;
using OscarsGame.Data.Interfaces;
using OscarsGame.Entities.StatisticsModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OscarsGame.Business
{
    public class BetsStatisticService : IBetStatisticService
    {
        private readonly IViewModelsRepository _viewModelsRepository;

        public BetsStatisticService(IViewModelsRepository viewModelsRepository)
        {
            _viewModelsRepository = viewModelsRepository;
        }

        public List<BetObject> GetData()
        {
            List<Winners> winners = _viewModelsRepository.GetWinner();

            Dictionary<string, List<BetMovieObject>> betsDict = new Dictionary<string, List<BetMovieObject>>();

            List<BetsStatistic> bets = _viewModelsRepository.GetBetsData();

            foreach (var bet in bets)
            {
                if (bet.Email != null)
                {
                    BetMovieObject betMovieObject = new BetMovieObject
                    {
                        CategoryTitle = bet.CategoryTitle,
                        MovieTitle = bet.MovieTitle,
                        IsRightGuess = winners.Any(x => x.Category == bet.CategoryTitle && x.Winner == bet.MovieTitle),
                    };

                    List<BetMovieObject> betMovieObjectList;
                    if (!betsDict.TryGetValue(bet.Email, out betMovieObjectList))
                    {
                        betMovieObjectList = new List<BetMovieObject>();
                        betsDict.Add(bet.Email, betMovieObjectList);
                    }

                    betMovieObjectList.Add(betMovieObject);
                }
            }

            return betsDict.Select(x => new BetObject { UserEmail = x.Key, UserBets = x.Value }).ToList();
        }

        public string GetWinner()
        {
            var users = GetData();
            List<Winners> winners = _viewModelsRepository.GetWinner();
            List<string[]> winningUsers = new List<string[]>();
            string currentWinnerName = String.Empty;
            int currentWinnerScores = 0;

            foreach (var user in users)
            {
                int scores = 0;
                foreach (var bet in user.UserBets)
                {
                    if (bet.IsRightGuess)
                    {
                        scores++;
                    }
                }

                if (scores == currentWinnerScores)
                {
                    currentWinnerScores = scores;
                    currentWinnerName = user.UserEmail;
                    string[] winningEntity = new string[2];
                    winningEntity[0] = currentWinnerName;
                    winningEntity[1] = currentWinnerScores.ToString();
                    winningUsers.Add(winningEntity);
                }

                if (scores > currentWinnerScores)
                {
                    currentWinnerScores = scores;
                    currentWinnerName = user.UserEmail;
                    string[] winningEntity = new string[2];
                    winningEntity[0] = currentWinnerName;
                    winningEntity[1] = currentWinnerScores.ToString();
                    winningUsers.Clear();
                    winningUsers.Add(winningEntity);
                }

            }

            return DisplayWinner(winningUsers);
        }

        private string DisplayWinner(List<string[]> winningUsers)
        {
            string resault = String.Empty;

            if (winningUsers.Count == 0)
            {
                resault = "<div class='redBorder'>There are no winners</div>";
                return resault;
            }
            else if (winningUsers.Count == 1)
            {
                var winner = winningUsers[0];
                resault = "<div class='redBorder'>The winner is " + winner[0] + "! He/she guessed right in " + winner[1] + " categories!</div>";
                return resault;
            }

            else
            {

                var finalWinners = new List<string[]>();
                var watchedMoviesService = new WatcheMoviesStatisticService(_viewModelsRepository);
                var usersMovies = watchedMoviesService.GetData();
                var arrayOfAllUsersWatchedMovies = usersMovies.Select(x => x.UserEmail).ToArray();
                var bestMovieCount = 0;
                foreach (var currentWinner in winningUsers)
                {
                    foreach (var user in usersMovies)
                    {

                        if (currentWinner[0] == user.UserEmail)
                        {
                            var currentUserWatchedMovies = user.MovieTitles;
                            if (currentUserWatchedMovies.Count == bestMovieCount)
                            {
                                string[] currentEntity = new string[2];
                                currentEntity[0] = user.UserEmail;
                                currentEntity[1] = bestMovieCount.ToString();
                                finalWinners.Add(currentEntity);
                            }
                            else if (currentUserWatchedMovies.Count > bestMovieCount)
                            {
                                string[] currentEntity = new string[2];
                                bestMovieCount = currentUserWatchedMovies.Count;
                                currentEntity[0] = user.UserEmail;
                                currentEntity[1] = bestMovieCount.ToString();
                                finalWinners.Clear();
                                finalWinners.Add(currentEntity);
                            }
                        }
                    }
                }
                if (finalWinners.Count == 0)
                {
                    resault = "<div class='redBorder'>The winners are: <br/>";
                    foreach (var winner in winningUsers)
                    {
                        resault += winner[0] + "<br/>";
                    }
                    resault += "They guessed rigth in " + winningUsers[0][1] + " categories!</div>";
                    return resault;
                }
                else if (finalWinners.Count == 1)
                {
                    resault = "<div class='redBorder'>" + finalWinners[0][0] + " wins! He/she guessed right in " + winningUsers[0][1] + " categories and watched " + finalWinners[0][1] + " movies!</div>";
                    return resault;
                }
                else
                {
                    resault = "<div class='redBorder'>The winners are: <br/>";
                    foreach (var winner in finalWinners)
                    {
                        resault += winner[0] + "<br/>";
                    }
                    resault += "They watched " + finalWinners[0][1] + " movies and guessed rigth in " + winningUsers[0][1] + " categories!</div>";
                    return resault;
                }
            }
        }

        public string[] GetCategories()
        {
            var dt = _viewModelsRepository.GetBetsData();
            var stringArrCategories = dt.Select(x => x.CategoryTitle).ToArray();
            var collectionWithDistinctCategories = stringArrCategories.Distinct().ToArray();
            return collectionWithDistinctCategories;
        }

        public List<Winners> GetWinners()
        {
            List<Winners> winners = _viewModelsRepository.GetWinner();
            return winners;
        }

    }
}
