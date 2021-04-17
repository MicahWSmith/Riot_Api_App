﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using Windows.UI.Xaml.Media.Imaging;

namespace ZillowAPIApp
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private FilterPage Page;
        public ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();
        public string SummonerName;
        public string Level;
        public string Tier;
        public string Rank;
        public string Wins;
        public string Losses;
        public string Icon;
        public string LP;
        public string TierRank;

        public event PropertyChangedEventHandler PropertyChanged;

        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get { return _currentUser; }

            set
            {
                _currentUser = value;
                if(value == null)
                {
                    SummonerName = "";
                    Level = "";
                    Tier = "";
                    Rank = "";
                    Wins = "";
                    Losses = "";
                    LP = "";
                    Icon = "";
                    TierRank = "";
                }
                else
                {
                    SummonerName = value.SummonerName;
                    Level = "Level: " + value.Level;
                    Tier = value.Tier;
                    Rank = value.Rank;
                    Wins = "Wins: " + value.Wins;
                    Losses = "Losses: " + value.Losses;
                    LP = value.LP + " LP";
                    Icon = value.Icon;
                    TierRank = value.Tier + " " + value.Rank;
                    Page.userPicture.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.8.1/img/profileicon/" + Icon + ".png", UriKind.Absolute));
                    Page.rankIcon.Source = new BitmapImage(new Uri("https://raw.githubusercontent.com/RiotAPI/Riot-Games-API-Developer-Assets/master/tier-icons/" + Tier.ToLower() + "_" + Rank.ToLower() + ".png", UriKind.Absolute));

                    Page.userName.Text = SummonerName;
                    Page.level.Text = Level;
                    Page.tierText.Text = TierRank;
                    Page.winsText.Text = Wins;
                    Page.lossesText.Text = Losses;
                    Page.lpText.Text = LP;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SummonerName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Level"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tier"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rank"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Wins"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Losses"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LP"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TierRank"));
            }
        }
        public UserViewModel(string search, FilterPage filterPage)
        {
            Page = filterPage;
            getStats(search);
        }

        public async void getStats(string username)
        { 
            if(username != "")
            {
                try
                {
                    var riotApi = RiotApi.NewInstance("RGAPI-0191fac7-8435-4e3b-bf64-00e9582be186");
                    var summonerInfo = await riotApi.SummonerV4.GetBySummonerNameAsync(Region.NA, username);
                    if(summonerInfo != null)
                    {
                        var rankedStats = await riotApi.LeagueV4.GetLeagueEntriesForSummonerAsync(Region.NA, summonerInfo.Id);
                        if(rankedStats[0].QueueType != "RANKED_SOLO_5x5")
                        {
                            SummonerName = summonerInfo.Name;
                            Level = summonerInfo.SummonerLevel.ToString();
                            Tier = rankedStats[1].Tier;
                            Rank = rankedStats[1].Rank;
                            Wins = rankedStats[1].Wins.ToString();
                            Losses = rankedStats[1].Losses.ToString();
                            LP = rankedStats[1].LeaguePoints.ToString();
                            Icon = summonerInfo.ProfileIconId.ToString();
                            TierRank = Tier + " " + Rank;
                            UserModel newUser = new UserModel(SummonerName, Level, Tier, Rank, Wins, Losses, Icon, LP);
                            CurrentUser = new UserModel(SummonerName, Level, Tier, Rank, Wins, Losses, Icon, LP);
                            userList.Add(newUser);
                        }
                        else
                        {
                            SummonerName = summonerInfo.Name;
                            Level = summonerInfo.SummonerLevel.ToString();
                            Tier = rankedStats[0].Tier;
                            Rank = rankedStats[0].Rank;
                            Wins = rankedStats[0].Wins.ToString();
                            Losses = rankedStats[0].Losses.ToString();
                            LP = rankedStats[0].LeaguePoints.ToString();
                            Icon = summonerInfo.ProfileIconId.ToString();
                            TierRank = Tier + " " + Rank;
                            UserModel newUser = new UserModel(SummonerName, Level, Tier, Rank, Wins, Losses, Icon, LP);
                            CurrentUser = new UserModel(SummonerName, Level, Tier, Rank, Wins, Losses, Icon, LP);
                            userList.Add(newUser);
                        }

                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SummonerName"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Level"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tier"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rank"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Wins"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Losses"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LP"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TierRank"));
                    }
                    
                }       
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                } 
            }
        }
    }
}