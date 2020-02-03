using NSOP_Tournament_Pro_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NSOP_Tournament_Pro
{
    /// <summary>
    /// Interaction logic for UserTournamentCreate.xaml
    /// </summary>
    public partial class UserTournamentCreate : UserControl
    {
        public ComboBox[] _CBX = new ComboBox[53];

        private Tournament _Tournament = new Tournament();
        public Tournament TournamentData { get => _Tournament; set => _Tournament = value; }

        public UserTournamentCreate()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            Fillcbx();
        }

        private void Fillcbx()
        {
            int _x = 0;
            _CBX[_x] = new ComboBox();
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Gametype - texas holdem
            _x++;
            _CBX[_x] = cbx_T1;
            _CBX[_x].ItemsSource = DataAccess.ListGameType();
            _CBX[_x].SelectedIndex = 0;
            // Gamevariant - no limit
            _x++;
            _CBX[_x] = cbx_T2;
            _CBX[_x].ItemsSource = DataAccess.ListVariant();
            _CBX[_x].SelectedIndex = 0;
            // GameStyle - Freexeout
            _x++;
            _CBX[_x] = cbx_T3;
            _CBX[_x].ItemsSource = DataAccess.ListStyle();
            _CBX[_x].SelectedIndex = 0;
            // Cost
            _x++;
            _CBX[_x] = cbx_T4;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Start chips
            _x++;
            _CBX[_x] = cbx_T5;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Fee
            _x++;
            _CBX[_x] = cbx_T6;
            _CBX[_x].ItemsSource = DataAccess.List10000();
            _CBX[_x].SelectedIndex = 0;
            // Late Reg Level
            _x++;
            _CBX[_x] = cbx_T7;
            _CBX[_x].ItemsSource = DataAccess.List10();
            _CBX[_x].SelectedIndex = 0;
            // Nationality
            _x++;
            _CBX[_x] = cbx_T8;
            _CBX[_x].ItemsSource = DataAccess.GetCountries();
            _CBX[_x].SelectedIndex = 0;
            // Age Gender
            _x++;
            _CBX[_x] = cbx_T9;
            _CBX[_x].ItemsSource = DataAccess.ListGenderAge();
            _CBX[_x].SelectedIndex = 0;
            // Members Only
            _x++;
            _CBX[_x] = cbx_T10;
            _CBX[_x].ItemsSource = DataAccess.ListMembership();
            _CBX[_x].SelectedIndex = 0;

            // TableSize
            _x++;
            _CBX[_x] = cbx_T11;
            _CBX[_x].ItemsSource = DataAccess.ListTable();
            _CBX[_x].SelectedIndex = 0;
            // MaxTables
            _x++;
            _CBX[_x] = cbx_T12;
            _CBX[_x].ItemsSource = DataAccess.ListTables();
            _CBX[_x].SelectedIndex = 0;
            // Starting Table
            _x++;
            _CBX[_x] = cbx_T13;
            _CBX[_x].ItemsSource = DataAccess.ListTables();
            _CBX[_x].SelectedIndex = 0;
            // remove from
            _x++;
            _CBX[_x] = cbx_T14;
            _CBX[_x].ItemsSource = DataAccess.ListHighLow();
            _CBX[_x].SelectedIndex = 0;
            // Rebuy QTY
            _x++;
            _CBX[_x] = cbx_T15;
            _CBX[_x].ItemsSource = DataAccess.List10();
            _CBX[_x].SelectedIndex = 0;
            // Rebuy Cost
            _x++;
            _CBX[_x] = cbx_T16;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Rebuy Fee
            _x++;
            _CBX[_x] = cbx_T17;
            _CBX[_x].ItemsSource = DataAccess.List10000();
            _CBX[_x].SelectedIndex = 0;
            // Rebuy Chips
            _x++;
            _CBX[_x] = cbx_T18;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Rebuy point
            _x++;
            _CBX[_x] = cbx_T19;
            _CBX[_x].ItemsSource = DataAccess.ListMinus();
            _CBX[_x].SelectedIndex = 0;
            // Addon QTY
            _x++;
            _CBX[_x] = cbx_T20;
            _CBX[_x].ItemsSource = DataAccess.List10();
            _CBX[_x].SelectedIndex = 0;
            // Addon Cost
            _x++;
            _CBX[_x] = cbx_T21;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Addon Fee
            _x++;
            _CBX[_x] = cbx_T22;
            _CBX[_x].ItemsSource = DataAccess.List10000();
            _CBX[_x].SelectedIndex = 0;
            // Addon Chips
            _x++;
            _CBX[_x] = cbx_T23;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Addon point
            _x++;
            _CBX[_x] = cbx_T24;
            _CBX[_x].ItemsSource = DataAccess.ListMinus();
            _CBX[_x].SelectedIndex = 0;
            // League Rounds
            _x++;
            _CBX[_x] = cbx_T25;
            _CBX[_x].ItemsSource = DataAccess.List60();
            _CBX[_x].SelectedIndex = 0;
            // Valid Rounds
            _x++;
            _CBX[_x] = cbx_T26;
            _CBX[_x].ItemsSource = DataAccess.List60();
            _CBX[_x].SelectedIndex = 0;
            // Round Fee
            _x++;
            _CBX[_x] = cbx_T27;
            _CBX[_x].ItemsSource = DataAccess.List10000();
            _CBX[_x].SelectedIndex = 0;
            // finale Tablesize
            _x++;
            _CBX[_x] = cbx_T28;
            _CBX[_x].ItemsSource = DataAccess.ListTable();
            _CBX[_x].SelectedIndex = 0;
            // finale point
            _x++;
            _CBX[_x] = cbx_T29;
            _CBX[_x].ItemsSource = DataAccess.ListTables();
            _CBX[_x].SelectedIndex = 0;
            // Round played
            _x++;
            _CBX[_x] = cbx_T30;
            _CBX[_x].ItemsSource = DataAccess.List60();
            _CBX[_x].SelectedIndex = 0;
            // min finale point
            _x++;
            _CBX[_x] = cbx_T31;
            _CBX[_x].ItemsSource = DataAccess.ListPoints();
            _CBX[_x].SelectedIndex = 0;
            // direct to final
            _x++;
            _CBX[_x] = cbx_T32;
            _CBX[_x].ItemsSource = DataAccess.List60();
            _CBX[_x].SelectedIndex = 0;
            // Semi round played
            _x++;
            _CBX[_x] = cbx_T33;
            _CBX[_x].ItemsSource = DataAccess.List60();
            _CBX[_x].SelectedIndex = 0;
            // Semi min point
            _x++;
            _CBX[_x] = cbx_T34;
            _CBX[_x].ItemsSource = DataAccess.ListPoints();
            _CBX[_x].SelectedIndex = 0;
            // Semi min point
            _x++;
            _CBX[_x] = cbx_T35;
            _CBX[_x].ItemsSource = DataAccess.List60();
            _CBX[_x].SelectedIndex = 0;
            // Minutes
            _x++;
            _CBX[_x] = cbx_T36;
            _CBX[_x].ItemsSource = DataAccess.List60();
            _CBX[_x].SelectedIndex = 0;
            // Hour
            _x++;
            _CBX[_x] = cbx_T37;
            _CBX[_x].ItemsSource = DataAccess.List24();
            _CBX[_x].SelectedIndex = 0;
            // Seve Deuce Point
            _x++;
            _CBX[_x] = cbx_T38;
            _CBX[_x].ItemsSource = DataAccess.ListMinPluss();
            _CBX[_x].SelectedIndex = 10;
            // Seve Deuce Chips
            _x++;
            _CBX[_x] = cbx_T39;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Seve Deuce QTY
            _x++;
            _CBX[_x] = cbx_T40;
            _CBX[_x].ItemsSource = DataAccess.List10();
            _CBX[_x].SelectedIndex = 0;
            // BadBeat Point
            _x++;
            _CBX[_x] = cbx_T41;
            _CBX[_x].ItemsSource = DataAccess.ListMinPluss();
            _CBX[_x].SelectedIndex = 10;
            // BadBeat Chips
            _x++;
            _CBX[_x] = cbx_T42;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // BadBeat QTY
            _x++;
            _CBX[_x] = cbx_T43;
            _CBX[_x].ItemsSource = DataAccess.List10();
            _CBX[_x].SelectedIndex = 0;
            // Hunted Point
            _x++;
            _CBX[_x] = cbx_T44;
            _CBX[_x].ItemsSource = DataAccess.ListMinPluss();
            _CBX[_x].SelectedIndex = 10;
            // Hunted Chips
            _x++;
            _CBX[_x] = cbx_T45;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Hunted QTY
            _x++;
            _CBX[_x] = cbx_T46;
            _CBX[_x].ItemsSource = DataAccess.List10();
            _CBX[_x].SelectedIndex = 0;
            // Takeout Point
            _x++;
            _CBX[_x] = cbx_T47;
            _CBX[_x].ItemsSource = DataAccess.ListMinPluss();
            _CBX[_x].SelectedIndex = 10;
            // Takeout Chips
            _x++;
            _CBX[_x] = cbx_T48;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Takeout QTY
            _x++;
            _CBX[_x] = cbx_T49;
            _CBX[_x].ItemsSource = DataAccess.List10();
            _CBX[_x].SelectedIndex = 0;
            // Time Card Point
            _x++;
            _CBX[_x] = cbx_T50;
            _CBX[_x].ItemsSource = DataAccess.ListMinPluss();
            _CBX[_x].SelectedIndex = 10;
            // Time Card Chips
            _x++;
            _CBX[_x] = cbx_T51;
            _CBX[_x].ItemsSource = DataAccess.List100000();
            _CBX[_x].SelectedIndex = 0;
            // Time Card QTY
            _x++;
            _CBX[_x] = cbx_T52;
            _CBX[_x].ItemsSource = DataAccess.List10();
            _CBX[_x].SelectedIndex = 0;
        }

        private void cbx_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as ComboBox).Focus();
        }

        private void cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex > -1)
            {
                switch ((sender as ComboBox).Name)
                {
                    case "cbx_T1": // Texas Holdem, Omaha . . . .
                        TournamentData.GameType = (sender as ComboBox).SelectedItem.ToString();
                        break;
                    case "cbx_T2": // No limit . . . .
                        TournamentData.GameLimits = (sender as ComboBox).SelectedItem.ToString();
                        break;
                    case "cbx_T3": // Freezeout . . . .
                        TournamentData.GameVariant = (sender as ComboBox).SelectedItem.ToString();
                        break;
                    case "cbx_T4": // Entry cost . .
                        TournamentData.EntryCost = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T5": // Startingchips .
                        TournamentData.StartingChips = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T6": // Club fee . . . .
                        TournamentData.EntryFee = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T7": // LateRegLevel . . . .
                        TournamentData.LateRegLevel = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T8": // Nationality . . . .
                        TournamentData.Nationality = (sender as ComboBox).SelectedItem.ToString();
                        break;
                    case "cbx_T9": // Gender . . . .
                        TournamentData.Gender = (sender as ComboBox).SelectedItem.ToString();
                        break;
                    case "cbx_T10": // Members . . . .
                        // shack all memberships and set them true 
                        switch ((sender as ComboBox).SelectedItem.ToString().ToUpper())
                        {
                            case "ANY":
                                TournamentData.ClubMembership = true;
                                TournamentData.NationalMembership = true;
                                TournamentData.NSOPMembership = true;
                                TournamentData.WorldMembership = true;
                                break;
                            case "CLUB":
                                TournamentData.ClubMembership = true;
                                break;
                            case "NSOP":
                                TournamentData.ClubMembership = true;
                                TournamentData.NSOPMembership = true;
                                break;
                            case "NATIONAL":
                                TournamentData.ClubMembership = true;
                                TournamentData.NationalMembership = true;
                                TournamentData.NSOPMembership = true;
                                break;
                            case "WORLD":
                                TournamentData.ClubMembership = true;
                                TournamentData.NationalMembership = true;
                                TournamentData.NSOPMembership = true;
                                TournamentData.WorldMembership = true;
                                break;
                        }

                        //TournamentData.ClubMembership = (sender as ComboBox).SelectedItem.ToString();
                        break;
                    case "cbx_T11": // Tablesize . . . .
                        TournamentData.TableSize = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T12": // MaxTables . . . .
                        TournamentData.MaxTables = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T13": // StartTable . . . .
                        TournamentData.StartTable = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T14": // RemoveTableHighLow . . . .
                        TournamentData.RemoveTableHighLow = (sender as ComboBox).SelectedItem.ToString();
                        break;
                    case "cbx_T15": // RebuyQty QTY . . . .
                        TournamentData.RebuyQty = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T16": // RebuyCost . . . .
                        TournamentData.RebuyCost = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T17": // RebuyFee . . . .
                        TournamentData.RebuyFee = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T18": // RebuyChips . . . .
                        TournamentData.RebuyChips = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T19": // RebuyPoints . . . .
                        TournamentData.RebuyPoints = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T20": // AddonQty . . . .
                        TournamentData.AddonQty = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T21": // AddonCost . . . .
                        TournamentData.AddonCost = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T22": // AddonFee . . . .
                        TournamentData.AddonFee = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T23": // AddonChips . . . .
                        TournamentData.AddonChips = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T24": // AddonPoints . . . .
                        TournamentData.AddonPoints = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T25": // Total League Rounds . . . .
                        TournamentData.TotalRounds = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T26": // Texas Holdem, Omaha . . . .
                        TournamentData.TotalRounds = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T27": // ValidRounds . . . .
                        TournamentData.ValidRounds = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T28": // FinalFee . . . .
                        TournamentData.FinalFee = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T29": // FinalTableSize . . . .
                        TournamentData.FinalTableSize = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T30": // FinalPayout . . . .
                        TournamentData.FinalPayout = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T31": // FinalMinRounds . . . .
                        TournamentData.FinalMinRounds = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T32": // FinalMinPoints . . . .
                        TournamentData.FinalMinPoints = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T33": // FinalDirect . . . .
                        TournamentData.FinalDirect = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T34": // SemiMinRounds . . . .
                        TournamentData.SemiMinRounds = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T35": // SemiMinPoints . . . .
                        TournamentData.SemiMinPoints = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T36": // SemiToFinalTickets . . . .
                        TournamentData.StartHour = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T37": // Texas Holdem, Omaha . . . .
                        TournamentData.StartMinuttes = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T38": // SevenDeuce Point . . . .
                        TournamentData.SevenDeucePoint = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T39": // SevenDeuce Chips . . . .
                        TournamentData.SevenDeuceChips = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T40": // SevenDeuce QTY . . . .
                        TournamentData.SevenDeuceChipsQTY = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T41": // Texas Holdem, Omaha . . . .
                        TournamentData.BadBeatPoint = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T42": // Texas Holdem, Omaha . . . .
                        TournamentData.BadBeatChips = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T43": // Texas Holdem, Omaha . . . .
                        TournamentData.BadBeatQTY = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T44": // Texas Holdem, Omaha . . . .
                        TournamentData.HuntedPoint = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T45": // Texas Holdem, Omaha . . . .
                        TournamentData.HuntedChips = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T46": // Texas Holdem, Omaha . . . .
                        TournamentData.HuntedQTY = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T47": // Texas Holdem, Omaha . . . .
                        TournamentData.TakeoutPoint = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T48": // Texas Holdem, Omaha . . . .
                        TournamentData.TakeoutChips = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T49": // Texas Holdem, Omaha . . . .
                        TournamentData.TakeoutQTY = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T50": // Texas Holdem, Omaha . . . .
                        TournamentData.TimeCardPoint = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T51": // Texas Holdem, Omaha . . . .
                        TournamentData.TimeCardChips = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                    case "cbx_T52": // Texas Holdem, Omaha . . . .
                        TournamentData.TimeCardQTY = Convert.ToInt16((sender as ComboBox).SelectedItem.ToString());
                        break;
                }
            }
        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void OpenAvatar_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnSaveTournament_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
