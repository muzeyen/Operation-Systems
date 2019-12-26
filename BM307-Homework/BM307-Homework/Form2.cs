using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BM307_Homework
{
    public partial class Form2 : Form
    {
        int number;  //oluşturulmak istenen sayı boyutu
        int memory_size; //Tablo boyutu
        int[] random_array; //oluşturulan sayıların dizisi
        bool found = false; //arama için
        public Form2(int number)
        {
            InitializeComponent();
            this.number = number;
        }

        private void Form2_Load(object sender, EventArgs e)
        {            
            findPrimary();
            label36.Text = number.ToString();
            random_array = new int[number];
            Random random = new Random();
            for(int i=0; i<number; i++)
            {
                random_array[i] = random.Next(1, 100);
                listBox1.Items.Add(i + ".sayı " + random_array[i] + " % " + memory_size + "= " + random_array[i] % memory_size);
            }
            eich();
            lisch();
            rlisch();
            beisch();
            eisch();
            lich();
        }
        public void findPrimary()
        {
            // dizi sayısından büyük, package factoru %95'ten küçük, en küçük asal sayı
            memory_size = number + 1;
        check:
            for (int i = 2; i < number; i++)
            {
                int kalan = memory_size % i;
                if (kalan == 0)
                {
                    memory_size++;
                    goto check;
                }
            }
            if ((float)number / (float)memory_size > 0.91)
            {
                memory_size++;
                goto check;
            }
            float pac_fac = (float)number / (float)memory_size;
            label32.Text = memory_size.ToString();
            label35.Text = pac_fac.ToString();
        }

        static int probe = 0;
        int search(int searching_number, int[] memory, int[] link)
        {
            for (int i = 0; i < memory_size; i++)
            {
                if (searching_number == memory[i]) //bulunursa
                {
                    found = true; //bulunduğunu işaretle
                }
            }
            if (found == false) //bulunmazsa -1 yap
                probe = -1;
            if (found == true) //bulunmuşsa
            {
                int home_adr = searching_number % memory_size; //home adresine bak
                if (searching_number == memory[home_adr]) //eğer homeda ise bir probe
                {
                    probe = 1;
                }
                else if (searching_number != memory[home_adr]) //değil ise 
                {
                    probe = 2;
                    while (searching_number != memory[link[home_adr]]) //bulana kadar linklere bak probe arttır
                    {
                        probe = probe + 1;
                        home_adr = link[home_adr];
                    }
                }
            }
            found = false; //başka aramalar için tekrar false yap
            return probe;
        }

        //BEISCH
        int beisch_counter = 0;
        int[] beisch_address;
        int[] beisch_link;
        public void beisch() //beischte home adres doluysa bir alttan bir üstten ekleme yapılır
        {
            beisch_address = new int[memory_size];
            beisch_link = new int[memory_size];
            bool turn = false;    // true --> üste ekler , false --> alta ekler
            for (int i = 0; i < memory_size; i++) //hepsini hiçbir değer yokmuş gibi yap
            {
                beisch_address[i] = -1;
                beisch_link[i] = -1;
            }
            int home_address;  // asıl adres değeri
            int son_index = memory_size - 1; // tablodaki en son index
            int top = 0; // tablonun en başındaki index
            for (int i = 0; i < random_array.Length; i++)
            {
                beisch_counter++;
                home_address = random_array[i] % (memory_size);  // home adres belirlenir
                if (beisch_address[home_address] == -1)//home adres boş ise, değer oraya atanır
                {
                    beisch_counter++;
                    beisch_address[home_address] = random_array[i];

                }
                else// home adres doluysa
                {
                    beisch_counter++;
                    if (beisch_link[home_address] == -1)  //adres dolu linki boşsa
                    {
                        beisch_counter++;
                        if (turn == true)  // üste yazma sırası gelmiş ise
                        {
                            beisch_counter++;
                            if (beisch_address[top] == 0) // en üst boş ise yaz
                            {
                                beisch_counter++;
                                beisch_address[top] = random_array[i];
                                beisch_link[home_address] = top;
                            }
                            else //en üst boş değil ise
                            {
                                beisch_counter++;
                                while (beisch_address[top] != -1) // en üstten aşağıya doğru inilerek, en üstteki boş indexe yaz
                                {
                                    beisch_counter++;
                                    top = top + 1;
                                }
                                beisch_address[top] = random_array[i];
                                beisch_link[home_address] = top;
                            }
                            turn = false; //sıranın altta olduğunu belirt
                        }

                        else if (turn == false) //alttan arama sırası
                        {
                            beisch_counter++;
                            if (beisch_address[son_index] == 0) //en alt boş ise yaz
                            {
                                beisch_counter++;
                                beisch_address[son_index] = random_array[i];
                                beisch_link[home_address] = son_index;
                            }
                            else //boş değilse
                            {
                                beisch_counter++;
                                while (beisch_address[son_index] != -1) //boş bulana kadar üste çık
                                {
                                    beisch_counter++;
                                    son_index = son_index - 1;
                                }
                                beisch_address[son_index] = random_array[i];
                                beisch_link[home_address] = son_index;
                            }
                            turn = true; //üstte sıra olduğunu belirt
                        }
                    }
                    else if (beisch_link[home_address] != -1)//home adres dolu linki de dolu ise
                    {
                        beisch_counter++;
                        if (turn == false) //alt kısımdan
                        {
                            beisch_counter++;
                            int temp = beisch_link[home_address];
                            while (beisch_address[son_index] != -1) //linki boş olanı bulana kadar git
                            {
                                son_index--;
                                beisch_counter++;
                            }
                            // early insertionda zincire eklenen son değer, zincirin ilk halkası gösterir
                            beisch_address[son_index] = random_array[i];
                            beisch_link[home_address] = son_index;
                            beisch_link[son_index] = temp;
                            turn = true;
                        }
                        else if (turn == true) //üst kısımdan
                        {
                            beisch_counter++;
                            int temp = beisch_link[home_address];
                            while (beisch_address[top] != -1)
                            {
                                beisch_counter++;
                                top++;
                            }
                            // early insertionda zincire eklenen son değer, zincirin ilk halkası gösterir
                            beisch_address[top] = random_array[i];
                            beisch_link[home_address] = top;
                            beisch_link[top] = temp;
                            turn = false;
                        }
                    }
                }
            }
            for (int i = 0; i < memory_size; i++) //listbox ekleme
            {
                listBox_BEISCH.Items.Add((i).ToString() + ".  " + beisch_address[i].ToString() + " |  " + beisch_link[i].ToString());
            }
            label8.Text = beisch_counter.ToString();  //toplam probe sayısını yazar
        }

        //LISCH
        int lisch_counter = 0;
        int[] lisch_address;
        int[] lisch_link;
        public void lisch() //lischte home addres doluysa yerleştirilmeye en alttan başlanır
        {
            lisch_address = new int[memory_size];
            lisch_link = new int[memory_size];
            for (int i = 0; i < memory_size; i++) //hepsini hiçbir değer yokmuş gibi yap
            {
                lisch_address[i] = (-1);
                lisch_link[i] = (-1);
            }
            int home_address;
            int son_index = memory_size - 1;
            for (int i = 0; i < random_array.Length; i++)
            {
                lisch_counter++;
                home_address = random_array[i] % (memory_size);
                if (lisch_address[home_address] == (-1)) // home adresi boş ise
                {
                    lisch_counter++;
                    lisch_address[home_address] = random_array[i];
                }
                else // home adresi boş değil ise
                {
                    lisch_counter++;
                    if (lisch_link[home_address] == (-1)) // homeadres dolu linki boş ise
                    {
                        lisch_counter++;
                        if (lisch_address[son_index] == (-1)) // en alt boş ise
                        {
                            lisch_counter++;
                            lisch_address[son_index] = random_array[i];
                            lisch_link[home_address] = son_index;
                        }
                        else  //en alt boş değil ise boş yer bulunana kadar en alttan yukarıya bak.
                        {
                            lisch_counter++;
                            while (lisch_address[son_index] != (-1)) //boş adres bulana kadar
                            {
                                lisch_counter++;
                                son_index = son_index - 1;
                            }
                            lisch_address[son_index] = random_array[i];
                            lisch_link[home_address] = son_index;
                        }
                    }
                    else // adres dolu linki de dolu
                    {
                        lisch_counter++;
                        while (lisch_link[home_address] != (-1)) // boş link bulana kadar 
                        {
                            lisch_counter++;
                            home_address = lisch_link[home_address];
                        }
                        if (lisch_address[son_index] == (-1)) //son index boş mu bak
                        {
                            lisch_counter++;
                            lisch_address[son_index] = random_array[i];
                            lisch_link[home_address] = son_index;
                        }
                        else //boş değil ise
                        {
                            lisch_counter++;
                            while (lisch_address[son_index] != (-1))
                            {
                                lisch_counter++;
                                son_index = son_index - 1;
                            }
                            lisch_address[son_index] = random_array[i];
                            lisch_link[home_address] = son_index;
                        }
                    }
                }
            }
            for (int i = 0; i < memory_size; i++) //listeye ekleme
            {
                listBox_LISCH.Items.Add((i).ToString() + ".  " + lisch_address[i].ToString() + " |  " + lisch_link[i].ToString());
            }
            label2.Text = lisch_counter.ToString(); //toplam probe sayısını yazma
        }

        //EISCH
        int eisch_counter = 0;
        int[] eisch_address;
        int[] eisch_link;
        public void eisch() //eischte home addres doluysa yerleştirilmeye en alttan başlanır
        {
            eisch_address = new int[memory_size];
            eisch_link = new int[memory_size];
            for (int i = 0; i < memory_size; i++) //hepsini hiçbir değer yokmuş gibi yap
            {
                eisch_address[i] = (-1);
                eisch_link[i] = (-1);
            }
            int home_address;
            int son_index = memory_size - 1;
            for (int i = 0; i < random_array.Length; i++)
            {
                eisch_counter++;
                home_address = random_array[i] % (memory_size);
                if (eisch_address[home_address] == (-1)) // home adresi boş ise
                {
                    eisch_counter++;
                    eisch_address[home_address] = random_array[i];
                }
                else // home adresi boş değil ise
                {
                    eisch_counter++;
                    if (eisch_link[home_address] == (-1)) // homeadres dolu linki boş ise
                    {
                        eisch_counter++;
                        if (eisch_address[son_index] == (-1)) // en alt boş ise
                        {
                            eisch_counter++;
                            eisch_address[son_index] = random_array[i];
                            eisch_link[home_address] = son_index;
                        }
                        else  //en alt boş değil ise boş yer bulunana kadar en alttan yukarıya bak.
                        {
                            eisch_counter++;
                            while (eisch_address[son_index] != (-1)) //boş adres bulana kadar
                            {
                                eisch_counter++;
                                son_index = son_index - 1;
                            }
                            eisch_address[son_index] = random_array[i];
                            eisch_link[home_address] = son_index;
                        }
                    }
                    else // adres dolu linki de dolu
                    {
                        eisch_counter++;
                        int temp = eisch_link[home_address];
                        while (eisch_address[son_index] != -1)
                        {
                            son_index--;
                            eisch_counter++;
                        }
                        // early insertionda zincire eklenen 3. veya sonraki değerleri, zincirin ilk halkası gösterir.(zincirin ikinci halkasına eklenir)
                        eisch_address[son_index] = random_array[i];
                        eisch_link[home_address] = son_index;
                        eisch_link[son_index] = temp;
                    }
                }
            }
            for (int i = 0; i < memory_size; i++) //listeye ekleme
            {
                listBox_EISCH.Items.Add((i).ToString() + ".  " + eisch_address[i].ToString() + " |  " + eisch_link[i].ToString());
            }
            label7.Text = eisch_counter.ToString(); //toplam probe sayısını yazma
        }

        //EICH
        int eich_counter = 0;
        int[] eich_address;
        int[] eich_link;
        public void eich() //eichte home address doluysa cellar içine yazmaya başlanır
        {
            eich_address = new int[memory_size];
            eich_link = new int[memory_size];
            for (int i = 0; i < memory_size; i++)  //hepsini hiçbir değer yokmuş gibi yap
            {
                eich_address[i] = (-1);
                eich_link[i] = (-1);
            }
            int home_address;
            int son_index = memory_size - 1;
            for (int i = 0; i < random_array.Length; i++)
            {
                eich_counter++;
                home_address = random_array[i] % (memory_size);
                if (eich_address[home_address] == (-1)) // home adres boş ise yaz
                {
                    eich_counter++;
                    eich_address[home_address] = random_array[i];

                }
                else // boş değil ise
                {
                    eich_counter++;
                    if (eich_link[home_address] == (-1)) //home adres dolu linki boş ise  
                    {
                        eich_counter++;
                        if (eich_address[son_index] == (-1)) //en alt adres boş ise
                        {
                            eich_counter++;
                            eich_address[son_index] = random_array[i];
                            eich_link[home_address] = son_index;
                        }
                        else // en alt boş değil ise
                        {
                            eich_counter++;
                            while (eich_address[son_index] != (-1)) //boş yer bulana kadar aşağıdan yukarıya doğru bak
                            {
                                eich_counter++;
                                son_index = son_index - 1;
                            }
                            eich_address[son_index] = random_array[i];
                            eich_link[home_address] = son_index;
                        }
                    }
                    else if (eich_link[home_address] != (-1))//adres dolu linki de dolu ise 
                    {
                        eich_counter++;
                        int temp = eich_link[home_address];
                        while (eich_address[son_index] != (-1)) //linki boş olanı bulana kadar bak
                        {
                            eich_counter++;
                            son_index--;
                        }
                        // early insertionda zincire eklenen son değer, zincirin ilk halkası gösterir
                        eich_address[son_index] = random_array[i];
                        eich_link[home_address] = son_index;
                        eich_link[son_index] = temp;
                    }
                }
            }
            for (int i = 0; i < memory_size; i++) //listeye ekleme
            {
                if (i == number) //alandan sonra overflow kısmını belirtmek için çiz
                    listBox_EICH.Items.Add("--------------cellar--------------");
                listBox_EICH.Items.Add((i).ToString() + ".  " + eich_address[i].ToString() + " |  " + eich_link[i].ToString());
            }
            label5.Text = eich_counter.ToString(); //toplam probe sayısı
        }

        //LICH
        int lich_counter = 0;
        int[] lich_address;
        int[] lich_link;
        public void lich() //eichte home address doluysa cellar içine yazmaya başlanır
        {
            lich_address = new int[memory_size];
            lich_link = new int[memory_size];
            for (int i = 0; i < memory_size; i++)  //hepsini hiçbir değer yokmuş gibi yap
            {
                lich_address[i] = (-1);
                lich_link[i] = (-1);
            }
            int home_address;
            int son_index = memory_size - 1;
            for (int i = 0; i < random_array.Length; i++)
            {
                lich_counter++;
                home_address = random_array[i] % (memory_size);
                if (lich_address[home_address] == (-1)) // home adres boş ise yaz
                {
                    lich_counter++;
                    lich_address[home_address] = random_array[i];

                }
                else // boş değil ise
                {
                    lich_counter++;
                    if (lich_link[home_address] == (-1)) //home adres dolu linki boş ise  
                    {
                        lich_counter++;
                        if (lich_address[son_index] == (-1)) //en alt adres boş ise
                        {
                            lich_counter++;
                            lich_address[son_index] = random_array[i];
                            lich_link[home_address] = son_index;
                        }
                        else // en alt boş değil ise
                        {
                            lich_counter++;
                            while (lich_address[son_index] != (-1)) //boş yer bulana kadar aşağıdan yukarıya doğru bak
                            {
                                lich_counter++;
                                son_index = son_index - 1;
                            }
                            lich_address[son_index] = random_array[i];
                            lich_link[home_address] = son_index;
                        }
                    }
                    else if (lich_link[home_address] != (-1))//adres dolu linki de dolu ise 
                    {
                        lich_counter++;
                        while (lich_address[son_index] != (-1)) //linki boş olanı bulana kadar bak
                        {
                            lich_counter++;
                            son_index--;
                        }
                        lich_address[son_index] = random_array[i];
                        lich_link[home_address] = son_index;
                    }
                }
            }
            for (int i = 0; i < memory_size; i++) //listeye ekleme
            {
                if (i == number) //alandan sonra overflow kısmını belirtmek için çiz
                    listBox_LICH.Items.Add("--------------cellar--------------");
                listBox_LICH.Items.Add((i).ToString() + ".  " + lich_address[i].ToString() + " |  " + lich_link[i].ToString());
            }
            label6.Text = eich_counter.ToString(); //toplam probe sayısı
        }

        //RLISCH
        int rlisch_counter = 0;
        int[] rlisch_address;
        int[] rlisch_link;
        public void rlisch() //home adres doluysa rastgele yapılır
        {
            rlisch_address = new int[memory_size];
            rlisch_link = new int[memory_size];
            for (int i = 0; i < memory_size; i++) //hepsini hiçbir değer yokmuş gibi yap
            {
                rlisch_address[i] = (-1);
                rlisch_link[i] = (-1);
            }
            Random rand = new Random();
            int home_address;
            int random_index = rand.Next(0, memory_size - 1); // rastgele üretilen adres indexi
            for (int i = 0; i < random_array.Length; i++)
            {
                rlisch_counter++;
                home_address = random_array[i] % (memory_size);
                if (rlisch_address[home_address] == (-1)) // home adresi boş ise 
                {
                    rlisch_counter++;
                    rlisch_address[home_address] = random_array[i];
                }
                else if (rlisch_address[home_address] != (-1)) // home adresi boş değilse
                {
                    rlisch_counter++;
                    if (rlisch_link[home_address] == (-1))  // adres dolu linki boş ise
                    {
                        rlisch_counter++;
                        while (rlisch_address[random_index] != (-1)) //adres boş olana kadar
                        {
                            rlisch_counter++;
                            random_index = rand.Next(0, memory_size - 1); //random index üret
                        }
                        rlisch_address[random_index] = random_array[i];
                        rlisch_link[home_address] = random_index;
                    }
                    else if (rlisch_link[home_address] != (-1)) // home adres de link de dolu ise
                    {
                        rlisch_counter++;
                        while (rlisch_link[home_address] != (-1)) //boş link ara
                        {
                            rlisch_counter++;
                            home_address = rlisch_link[home_address];
                        }
                        while (rlisch_address[random_index] != -1) // boş link bulunca, boş adres bulana kadar
                        {
                            rlisch_counter++;
                            random_index = rand.Next(0, memory_size - 1); //rastgele index üret
                        }
                        rlisch_address[random_index] = random_array[i];
                        rlisch_link[home_address] = random_index;
                    }
                }
            }
            for (int i = 0; i < memory_size; i++) //listeye ekleme
            {
                listBox_RLISCH.Items.Add((i).ToString() + ".  " + rlisch_address[i].ToString() + " |  " + rlisch_link[i].ToString());
            }
            label4.Text = rlisch_counter.ToString(); //toplam probe sayısı
        }

        private void label39_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            int searching_number = Convert.ToInt32(textBox_search.Text);
            int lisch_probe, beisch_probe, rlisch_probe, eich_probe,eisch_probe,lich_probe;
            lisch_probe = search(searching_number, lisch_address, lisch_link);
            eisch_probe = search(searching_number, eisch_address, eisch_link);
            beisch_probe = search(searching_number, beisch_address, beisch_link);
            rlisch_probe = search(searching_number, rlisch_address, rlisch_link);
            eich_probe = search(searching_number, eich_address, eich_link);
            lich_probe= search(searching_number, lich_address, lich_link);

            label23.Text = (" " + beisch_probe.ToString());
            label21.Text = (" " + rlisch_probe.ToString());
            label31.Text= (" " + eisch_probe.ToString());
            label25.Text = (" " + eich_probe.ToString());
            label30.Text = (" " + lisch_probe.ToString());
            label22.Text = (" " + lich_probe.ToString());
        }      
    }
}
