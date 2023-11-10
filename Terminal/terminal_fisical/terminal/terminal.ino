/*
PINOUT:
RC522 MODULE    Uno/Nano     MEGA
SDA             D10          D9
SCK             D13          D52
MOSI            D11          D51
MISO            D12          D50
IRQ             N/A          N/A
GND             GND          GND
RST             D9           D8
3.3V            3.3V         3.3V
*/
#include <SPI.h>
#include <RFID.h>
#include <LiquidCrystal.h>
#include <Keypad.h>

//payS1 ID 236 
//payS2 ID 24 131
//bank1 ID 40 43
//bank2 ID 121
//ok 1
//not enough 2
//transmitt error 3


#define SDA_DIO 9
#define RESET_DIO 8
RFID RC522(SDA_DIO, RESET_DIO); 
unsigned long int ser=0;

int const rs=44, en=45, d4=48, d5=49, d6=46, d7=47;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

const byte ROWS = 4; //four rows
const byte COLS = 4; //three columns
char keys[ROWS][COLS] = {
  {1,2,3,4},
  {5,6,7,8},
  {9,10,11,11},
  {13,'#','*',16}
};
byte rowPins[ROWS] = {26, 27, 28, 29}; //connect to the row pinouts of the keypad
byte colPins[COLS] = {22, 23, 24, 25}; //connect to the column pinouts of the keypad

Keypad keypad = Keypad( makeKeymap(keys), rowPins, colPins, ROWS, COLS );

char key;
unsigned char amount[8]={11,11,11,11,11,11,11,11};
unsigned char temp[8]={11,11,11,11,11,11,11,11};
int offset=0;
int iAmount=0;


int process=0;
unsigned int cardNumber[5]={0,0,0,0,0};
unsigned int recipientCardNumber[5]={192,12,173,0,1}; 

unsigned int outBuffer[27]={0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
unsigned int CRC=0;
int result=0;


void setup()
{ 
  Serial.begin(9600);
  SPI.begin(); 
  RC522.init();
  lcd.begin(16,2);

      lcd.setCursor(0, 0); 
      lcd.clear();
      lcd.print("rus warshEEp");
      lcd.setCursor(0, 1); 
      lcd.print("fuck yourself!");

   ReadCard();
   for(int i=0;i<5;i++)
    {
      Serial.write(cardNumber[i]);
      delay(100);
    }
    for(int i=0;i<5;i++)
    {
      cardNumber[i]=0;
    }

    delay(5000);
}

int ReadKey()
{
   iAmount=0;
   bool stop=false;
   int keyCount=0;
   int result=0;
   for(int i=0;i<8;i++)
   {
    amount[i]=11;
   }
   

   while(!stop)
   {
       key = keypad.getKey();
       if(key!=NO_KEY)
       {
        if(iAmount<8 && key<11)
        {
          if(key==10)
          {
            key=0;
          }
          if(iAmount==0 && key==0)
          {

          }
          else
          {
            amount[iAmount]=key;
            iAmount++;
            lcd.clear();
            lcd.print("Enter Amount:");
            lcd.setCursor(13,1);
            lcd.print("UAH");
            lcd.setCursor(11-offset,1);
            offset++;
            for(int i=0;i<8;i++)
            {
             if(amount[i]!=11)
             {
              lcd.print(amount[i],DEC); 
             }
            }
          }

        }
        
        if(key==13)
        {
         stop=true;
         lcd.clear();
         offset=0;
         result=13;
        }
        if(key==16)
        {
         stop=true;
         lcd.clear();
         offset=0;
         result=16;
        }
       }

   } 

return result;
}

void ReadCard()
{
 if (RC522.isCard())
  {
    RC522.readCardSerial();  
    for(int i=0;i<5;i++)
    {
      cardNumber[i]=RC522.serNum[i];
    }
    if(cardNumber[3]==121)
    {
     cardNumber[3]=0;
    }
    if(cardNumber[3]==43)
    {
      cardNumber[3]=2;
    }
    if(cardNumber[3]==40)
    {
      cardNumber[3]=1;
    }
    if(cardNumber[4]==24)
    {
      cardNumber[4]=1;
    }
    if(cardNumber[4]==131)
    {
      cardNumber[4]=2;
    }
    if(cardNumber[4]==236)
    {
      cardNumber[4]=0;
    }        

  }
}

void loop()
{
  if(process==0)
  {
   lcd.setCursor(0, 0); 
   lcd.clear();
   lcd.print("Idle....");
   int result=ReadKey();
   if(result==16)
   {
    process=1;
   }
  }

  if(process==1)
  {
    lcd.setCursor(0, 0); 
    lcd.clear();
    lcd.print("Waiting card...");
    for(int i=0;i<100;i++)
    {
      ReadCard();
      if(cardNumber[0]!=0 || cardNumber[1]!=0 || cardNumber[2]!=0 || cardNumber[3]!=0 || cardNumber[4]!=0)
      {
        i=100;
        process=2;
      }
       key = keypad.getKey();
       if(key!=NO_KEY)
       {
        if(key==13)
        {
         i=100;
         lcd.setCursor(0, 0); 
         lcd.clear();
         lcd.print("Canceled!");
         delay(2000);
         process=0;
        }
       }
      delay(50);
    }
   if(process==1)
    {
     lcd.setCursor(0, 0); 
     lcd.clear();
     lcd.print("Card timeout!");
     delay(2000);
     process=0;
    }
    
  }

  if(process==2)
  {
     lcd.setCursor(0, 0); 
     lcd.clear();
     lcd.print("Connecting");
     lcd.setCursor(0, 1);
     lcd.print("to bank...");
     for(int i=3;i<8;i++)
     {
      outBuffer[i]=cardNumber[i-3];
     }
     for(int i=11;i<16;i++)
     {
      outBuffer[i]=recipientCardNumber[i-11];
     }
     for(int i=16;i<24;i++)
     {
      outBuffer[i]=amount[i-16];
     }
     outBuffer[24]=0;
     CRC=0;
     for(int i=0;i<25;i++)
     {
      CRC+=outBuffer[i];
     }
     outBuffer[25]=CRC>>8;
     outBuffer[26]=CRC;

     for(int i=0;i<27;i++)
     {
      Serial.write(outBuffer[i]);
      delay(100);
     }

     result=0;
     for(int i=0;i<100;i++)
     {
      result=Serial.read();
      if(result!=-1)
      {
        i=100;
      }
      delay(50);
     }
     
     if(result==-1)
     {
      lcd.setCursor(0, 0); 
      lcd.clear();
      lcd.print("Bank not");
      lcd.setCursor(0, 1); 
      lcd.print("responding!");
     }
     if(result==1)
     {
       lcd.setCursor(0, 0); 
       lcd.clear();
       lcd.print("OK!");
     }
     if(result==2)
     {
      lcd.setCursor(0, 0); 
      lcd.clear();
      lcd.print("Balance not");
      lcd.setCursor(0, 1); 
      lcd.print("enough!");
     }    
     if(result==3)
     {
      lcd.setCursor(0, 0); 
      lcd.clear();
      lcd.print("Transmitt");
      lcd.setCursor(0, 1); 
      lcd.print("error!");
     } 
     if(result==4)
     {
      lcd.setCursor(0, 0); 
      lcd.clear();
      lcd.print("rus warshEEp");
      lcd.setCursor(0, 1); 
      lcd.print("fuck yourself!");
     } 

     delay(5000);

     for(int i=0;i<5;i++)
     {
      cardNumber[i]=0;
     }
     for(int i=0;i<8;i++)
     {
      amount[i]=0;
     }
     process=0;
  }
   

}


















//delay(100);

  /*
  if (RC522.isCard())
  {
    RC522.readCardSerial();
    lcd.clear();
    //lcd.print(RC522.serNum[1],HEX);
      //Serial.print(RC522.serNum[1],HEX);
    
    for(int i=0;i<5;i++)
    {
      lcd.setCursor(i*2,0);
      lcd.print(RC522.serNum[i],HEX);
      Serial.print(RC522.serNum[i],HEX);
    }
    Serial.println();
  }
  delay(1000);*/
