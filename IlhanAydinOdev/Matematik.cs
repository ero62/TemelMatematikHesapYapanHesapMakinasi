using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IlhanAydinOdev
{
    class Matematik
    {
        public int mutlakdeger(int sayi)
        {
            
            if (sayi < 0)
            {
                sayi *= -1;
                return sayi;
            }
            else
            {
                return sayi;
            }   
        }
        public int MaxHesapla(string[] str)
        {
            List<int> sayilar = new List<int>(); // Sayıları saklamak için bir liste oluştur

            foreach (var giris in str)
            {
                // Girilen değeri int olarak dönüştürmeyi dene
                if (int.TryParse(giris.Trim(), out int sayi))
                {
                    sayilar.Add(sayi); // Dönüşüm başarılıysa listeye ekle
                }
                else
                {
                    // Hatalı giriş varsa kullanıcıyı uyar ve işlemi durdur
                    MessageBox.Show($"Geçersiz giriş: {giris}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return int.MinValue; // Hatalı giriş olduğunda bir hata değeri döndür
                }
            }

            if (sayilar.Count == 0) // Eğer sayılar listesi boşsa, hata veya boş dizi durumu
            {
                MessageBox.Show("Sayı listesi boş.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return int.MinValue; // Listede sayı yoksa bir hata değeri döndür
            }

            int enBuyuk = sayilar[0]; // En büyük sayıyı bulmak için ilk elemanı başlangıç değeri olarak al
            foreach (var sayi in sayilar)
            {
                if (sayi > enBuyuk)
                {
                    enBuyuk = sayi; // Daha büyük bir sayı bulunduğunda, en büyük değeri güncelle
                }
            }
            return enBuyuk; // En büyük sayıyı döndür
        }
        public double KarekokAl(int sayi)
        {
            if (sayi < 0)
            {
                MessageBox.Show("Lütfen geçerli bir sayı giriniz.");
                return 0; // Geçersiz giriş için hata değeri döndür
            }

            double epsilon = 0.0001;
            double tahmin = sayi / 2.0;

            while (true)
            {
                double yeniTahmin = 0.5 * (tahmin + sayi / tahmin);
                if (Math.Abs(yeniTahmin - tahmin) < epsilon)
                {
                    tahmin = yeniTahmin;
                    break;
                }
                tahmin = yeniTahmin;
            }
            return tahmin;
        }
        private double CosineTaylorSeries(double x)
        {
            const int N = 20; // Daha fazla terim için N'i arttırdım
            double sum = 1.0;
            double powerOfX = 1.0;
            double factorial = 1.0;

            for (int i = 1; i <= N; i++)
            {
                powerOfX *= x * x; // x^2, x^4, x^6, ...
                factorial *= (2 * i - 1) * (2 * i); // (2!), (4!), (6!), ...
                sum += powerOfX / factorial * (i % 2 == 0 ? 1 : -1); // İşaret değişimi
            }

            return sum;
        }
        private double SineTaylorSeries(double x)
        {
            const int N = 50; // Yakınsama için daha fazla terim
            double sum = 0.0;
            double term = x; // İlk terim x

            for (int i = 1; i <= N; i++)
            {
                sum += term;
                term *= -1 * x * x / (2 * i * (2 * i + 1));
            }

            return sum;
        }
        public double sinHesapla(double aci)
        {
            // Radyana çevir
            aci = aci * (Math.PI / 180.0);

            // Sinüs değerini Taylor serisi ile hesapla
            double sinValue = SineTaylorSeries(aci);

            // Hesaplanan sinüs değerini TextBox'a yaz
            double sonuc = Math.Round(sinValue, 14);
            return sonuc;
        }
        public double cosHesapla(double aci)
        {
            //// Radyana çevir (C# içindeki Math.Sin, Math.Cos fonksiyonları radyan cinsinden hesaplama yapar)
            aci = aci * (Math.PI / 180.0);

            //// Kosinüs değerini Taylor serisi ile hesapla
            double cosValue = CosineTaylorSeries(aci);

            //// Hesaplanan kosinüs değerini TextBox'a yaz
            double sonuc = Math.Round(cosValue, 14); // 14 basamağa yuvarla
            return sonuc;
        }
    }
}
