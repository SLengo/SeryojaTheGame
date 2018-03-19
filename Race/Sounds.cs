using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Resources;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using System.Windows.Media.Animation;

namespace Race
{
    public class Sounds
    {
        public static string BackGroundSoundUri = "sounds/background_music.wav";
        public static string ShootLaserSoundUri = "sounds/laser_shoot.wav";
        public static string HpBonusSoundUri = "sounds/hp_bonus.wav";
        public static string LaserBonusSoundUri = "sounds/laser_bonus.wav";
        public static string ShipDamageSoundUri = "sounds/ship_damage.wav";
        public static string ObstDamageSoundUri = "sounds/obst_damage.wav";
        public static string GameOverSoundUri = "sounds/game_over.wav";
        public static string ShipDestroySoundUri = "sounds/ship_destroy.wav";
        public static string EmpLaserSoundUri = "sounds/emp_laser.wav";

        public static MediaPlayer BackGroundMediaPlayer;
        public static MediaPlayer ShootMediaPlayer;
        public static MediaPlayer HpBonusMediaPlayer;
        public static MediaPlayer LaserBonusMediaPlayer;
        public static MediaPlayer ShipDamageMediaPlayer;
        public static MediaPlayer ObstDamageMediaPlayer;
        public static MediaPlayer GameOverMediaPlayer;
        public static MediaPlayer ShipDestroyMediaPlayer;
        public static MediaPlayer EmpLaserMediaPlayer;

        public static void PlayBackGround()
        {
            if (BackGroundMediaPlayer != null)
            {
                BackGroundMediaPlayer.Stop();
            }
            BackGroundMediaPlayer = new MediaPlayer();
            BackGroundMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + BackGroundSoundUri));
            BackGroundMediaPlayer.Volume = 50;
            BackGroundMediaPlayer.Play();
        }
        public static void StopBackGround()
        {
            if (BackGroundMediaPlayer == null) return;
            BackGroundMediaPlayer.Stop();
        }
        public static void LaserShootSoundPlay()
        {
            ShootMediaPlayer = new MediaPlayer();
            ShootMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + ShootLaserSoundUri));
            ShootMediaPlayer.Volume = 70;
            ShootMediaPlayer.Play();
        }
        public static void HpBonusSoundPlay()
        {
            HpBonusMediaPlayer = new MediaPlayer();
            HpBonusMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + HpBonusSoundUri));
            HpBonusMediaPlayer.Volume = 70;
            HpBonusMediaPlayer.Play();
        }
        public static void LaserBonusSoundPlay()
        {
            LaserBonusMediaPlayer = new MediaPlayer();
            LaserBonusMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + LaserBonusSoundUri));
            LaserBonusMediaPlayer.Volume = 70;
            LaserBonusMediaPlayer.Play();
        }
        public static void ShipDamageSoundPlay()
        {
            ShipDamageMediaPlayer = new MediaPlayer();
            ShipDamageMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + ShipDamageSoundUri));
            ShipDamageMediaPlayer.Volume = 70;
            ShipDamageMediaPlayer.Play();
        }
        public static void ObstDamageSoundPlay()
        {
            ObstDamageMediaPlayer = new MediaPlayer();
            ObstDamageMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + ObstDamageSoundUri));
            ObstDamageMediaPlayer.Volume = 70;
            ObstDamageMediaPlayer.Play();
        }
        public static void ShipDestroySoundPlay()
        {
            ShipDestroyMediaPlayer = new MediaPlayer();
            ShipDestroyMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + ShipDestroySoundUri));
            ShipDestroyMediaPlayer.Volume = 70;
            ShipDestroyMediaPlayer.Play();
        }
        public static void EmpLaserSoundPlay()
        {
            EmpLaserMediaPlayer = new MediaPlayer();
            EmpLaserMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + EmpLaserSoundUri));
            EmpLaserMediaPlayer.Volume = 70;
            EmpLaserMediaPlayer.Play();
        }

        public static void GameOverSoundPlay()
        {
            if (GameOverMediaPlayer != null)
            {
                GameOverMediaPlayer.Stop();
            }
            GameOverMediaPlayer = new MediaPlayer();
            GameOverMediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + GameOverSoundUri));
            GameOverMediaPlayer.Volume = 50;
            GameOverMediaPlayer.Play();
        }
        public static void GameOverSoundStop()
        {
            if (GameOverMediaPlayer == null) return;
            GameOverMediaPlayer.Stop();
        }
    }
}
