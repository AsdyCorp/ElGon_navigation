



byte buf[4];
byte Pins[] = {A0, A1, A2, A3};
void setup() {
    Serial.begin(9600);

    pinMode(A0, OUTPUT); 
    pinMode(A1, OUTPUT); 
    pinMode(A2, OUTPUT); 
    pinMode(A3, OUTPUT); 
 
   
     analogWrite(A0, 0);
      analogWrite(A1, 0);
       analogWrite(A2, 0);
        analogWrite(A3, 0);
        
}


void loop () {
    if (Serial.available() > 0) {
      
      int rlen = Serial.readBytesUntil(23, buf, 4);
      for(int i=0; i<4; i++)
      {
        analogWrite(Pins[i], buf[i]);
      }

    }
    
    
}
