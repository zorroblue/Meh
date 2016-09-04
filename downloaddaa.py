import urllib2

base_url = "http://www.facweb.iitkgp.ernet.in/~sourav/"

def downloadfile(url, filename):
    response= urllib2.urlopen(url)
    file=open(filename,'w')
    file.write(response.read())
    file.close()
    print filename+" is complete"

def main():
    i=1
    filename=""
    for i in range(1,23):
        if i<10:
            filename = "Lecture-0"+str(i)+".pdf"
        else:
            filename = "Lecture-"+str(i)+".pdf"
        downloadfile(base_url+filename,filename)

if __name__ == "__main__":
    main()
        
        
