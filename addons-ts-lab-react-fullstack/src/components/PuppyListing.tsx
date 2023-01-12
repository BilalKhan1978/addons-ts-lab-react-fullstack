import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

// call two methods one is for  Get All
// second method is for deleting a contact. Delete method

const PuupyListing = () => {
    const [contdata,contdatachange]= useState<Puppy[]>([]);
    const defaultImageSrc = '/img/image_placeholder.png'
    class Puppy {
        id:string="";
        name:string="";
        breed:string="";
        birthDate:string="";
        imageSrc:string='/img/image_placeholder.png';
      }  

    const navigate=useNavigate();
    const Removefunction=( id:string)=>{
        if(window.confirm("Do you really want to delete?"))
        {
            // delete endpoint method
            fetch("https://localhost:7154/api/Puppy/"+id,{
                method:"DELETE"       
                }).then((res)=>{
                    alert('Deleted successfully.')
                    window.location.reload();
                    }).catch((err)=>{
                        console.log(err.message)
                        })
        }
    }

    const LoadEdit=(id: string)=>{
        navigate('/contacts/edit/'+id);
    }
           // get all endpoint method
    useEffect(()=>{
        fetch("https://localhost:7154/api/Puppy").then ((res)=>{
            return res.json();
        }).then((resp:Puppy[])=>{
            resp.forEach(element => {
                if(element.imageSrc==null)
                element.imageSrc=defaultImageSrc;
            });
            contdatachange(resp);
        }).catch((err)=>{
            console.log(err.message);
        })
    },[])
    return(
        <div>
             <br/>
            <div className="row">
               
                <div className="col-md-6">
                <h2 >Puppies for sale</h2> 
                </div>
                <div className="col-md-6">
                
                <a href="/contacts/create" className="float-right">
                <button>Add New +</button>
                </a>
                </div>
            </div>
            <div>
            <br/>
                <br/>
                <div className="row">
                    <div className="col-md-12">
                    <div className="container text-center">
                        
                        { contdata &&
                            contdata.map(item=>(
                                <div className="row div-border">                                
                                <div className="col-md-12"><h4>{item.name}</h4></div>  
                                <div className="col-md-12"><h5>{item.breed}</h5></div> 
                                <div className="col-md-12"><h6>{item.birthDate}</h6></div>
                                <div className="col-md-12"><img src={item.imageSrc} width={250} height={250}  /></div>  
                                <br/>
                                <div className="col-md-6">
                                <a style={{color: "blue"}} className="float-right" onClick={()=> {LoadEdit(item.id)}}>Edit     </a>
                                </div>
                                <div className="col-md-6">
                                <a style={{color: "red"}} className="float-left" onClick={()=> {Removefunction(item.id)}}>    Delete</a>
                               
                                </div>
                                                 
                                
                                 
                                </div> 

                                    
                            ))
                        }
                    </div>
                    </div>
                </div>                
            </div>
        </div>
    );
}

export default PuupyListing;