#include<iostream>
using namespace std;

//uses the ownership policy i.e the memory can have multiple objects
template<class T>
class SmartPtr{
    private:
        T *pointee_;
        int count;
    public:
        explicit SmartPtr(T *pointee):pointee_(pointee),count(0);
        
        SmartPtr(SmartPtr &src)
        {
            pointee_=src.pointee_;
            //src.pointee_=0;
            pointee->count++;
        }
        SmartPtr& operator=(SmartPtr& src)
        {
            if(this!=&src)
            {
                delete pointee_;
                pointee_=src.pointee_;
                pointee->count++;
            }
            return *this;
        }
        ~SmartPtr()
        {
            if(pointee_->count==1) //last pointer
            {
                //delete it
                delete pointee_;
            }
            else
            {
                pointee_->count--;
            }
        }

