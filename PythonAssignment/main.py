import re;
import MySQLdb;
import traceback;

#text parsing
f=open('data.txt','r')
#print f.read()
recordList= re.split(r'_|:|\n',f.read())
i=0
#fw=open('result.txt','w')
sql=""
db=MySQLdb.connect("localhost","root","user123","nikiai")

cursor=db.cursor()
try:
    cursor.execute("create table record(phone_number int(10), date varchar(30),sentence varchar(50));")
except:
    a11=1; #do nothing
#   print"Your database connection is not right..."
#traceback.print_exc();
#    exit(0)
#clear table
try:
    cursor.execute("truncate table record")
except:
    a12=1 #do nothing
for record in recordList:
    if(i%6==1):
        a1=record
    if(i%6==3):
        a2=record
    if(i%6==5):
        a3=record.replace('.','')
        #save record
        sql="INSERT INTO record(phone_number,date,sentence) values('%s','%s','%s')" % (a1,a2,a3);
        try:
            cursor.execute(sql)
            db.commit()
        except:
            traceback.print_exc()
            db.rollback()
    i=i+1




#get queries
flag=1;
while(flag):
    print"Menu\n1.Find by date\n2.Filter by combination\n3.Exit"
    try:
        i=eval(raw_input())
    except:
        continue
    if(i==1):
        print"Enter the date"
        date_input=raw_input();
        sql="SELECT * FROM record where date='%s' order by phone_number " % date_input;
        try:
            cursor.execute(sql);
            results=cursor.fetchall()
#            print results
            i=0
            old_number=""
            if(not results):
                print"\nNo results found!\n"
                continue
            for row in results:
                    if(old_number!=str(row[0])):
                        print"\nPhone number: "+str(row[0])
                        old_number=str(row[0])
                    print "Conversation: "+str(row[2]);     
            print"\n"
        except:
            print "There was an error in processing your request"
            db.rollback()
    elif(i==2):
        print "Enter the phone number"
        phone=input()
        print "Enter the domain name"
        domain_name="abc"
        domain_name=raw_input()
        flag1=False
        print"\n"
        sql="SELECT * FROM record where phone_number=%s" %str(phone);
        try:
            cursor.execute(sql);
            results=cursor.fetchall();
            i=0
            for row in results:
                p=row[0]
                q=row[1]
                r=row[2]
                if domain_name in r.split():
                    print "Date :"+str(q)+"\nConversation: "+str(row[2])+"\n";
                    flag1=True
            if(flag1==False):
                print"No results found\n"
            else:
                print"\n";
        except:
            print "There was an error in processing your request"
            db.rollback()
    elif(i!=1 and i!=2):
        exit(0)
    else:
        print"Invalid choice!\n\n"
        
db.close()
