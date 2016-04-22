#include<iostream>
using namespace std;

template<class T>
class SmartPtr{
    private:
        T *pointee_;
    public:
        explicit SmartPtr(T *pointee)
        {
            this.pointee_=pointee;
        }
        
        SmartPtr(SmartPtr &src)
        {
            pointee_=src.pointee_;
            src.pointee_=0;
        }
        SmartPtr& operator=(SmartPtr& src)
        {
            if(this!=&src)
            {
                delete pointee_;
                pointee_=src.pointee_;
                src.pointee_=0;
            }
            return *this;
        }
        
