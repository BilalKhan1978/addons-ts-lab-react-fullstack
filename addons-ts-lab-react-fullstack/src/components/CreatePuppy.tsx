import { useNavigate } from "react-router-dom";
import { useState } from "react";
import http from "../http-common";
const CreatePuppy = () =>{

  // call a method that is POST
    
   
    type Puppy = {
      name: string,
      breed: string,
      birthDate: string,
      imageSrc:string,
      image: string|File
  }
    
  const [values, setValues] = useState<Puppy>({
    name: '',
      breed: '',
      birthDate: '',
      imageSrc:'/img/image_placeholder.png',
      image: ''
  });
  function handleInputChange(e:any)  {
    let value  = e.target.value;
    setValues({
        ...values,
        [e.target.id]: value
    })
}
    

function showPreview(e: any)  {
  if (e.target.files && e.target.files[0]) {
      let image = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (ev: any) => {
          setValues({
              ...values,
              image,
              imageSrc: reader.result as string
          })
      }
      reader.readAsDataURL(image)
  }
    
}
    const navigate=useNavigate();

    const handelsubmit = (e: any) =>{
        e.preventDefault();
        const formData = new FormData()
        formData.append('name', values.name)
        formData.append('breed', values.breed)
        formData.append('birthDate', values.birthDate)
        formData.append('image', values.image)
        http.post("/api/Puppy", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
          
        }).then((response) => {
          if(response)
          {
                alert('Saved successfully.')
                navigate('/');
                return;
              }  
        });  

    }

return(
    <div className="container ">
      <br/>
           <div >
                    <div>
                        <h2>Add new puppy</h2>
                    </div>
                    <br/>
                    <form onSubmit={handelsubmit} className="div-border">
                        <div className="row">
                        <div className="form-group col-md-1">
                        <label>Name* </label>
                        </div>
                        <div className="col-md-2">
                        <input value={values.name} id="name" onChange={handleInputChange}></input>
                        </div>
                        </div>
                        <div className="row">
                        <div className="form-group col-md-1">
                        <label>Breed*</label>
                        </div>
                        <div className="col-md-2">
                        <input value={values.breed} id="breed" onChange={handleInputChange}></input>
                        </div>
                        </div>
                        <div className="row">
                        <div className="form-group col-md-1">
                        <label>BirthYear* </label>
                        </div>
                        <div className="col-md-2">
                        <input value={values.birthDate} id="birthDate" onChange={handleInputChange}></input>
                        </div>
                        </div>
                        <br/>
                        <div className="row">
                        <div className="form-group col-md-1">
                        <label>Image </label>
                        
                        </div>
                                             
                        
          <div className="col-md-2">
          <input type="file" id="image" accept="image/*" onChange={showPreview} />      
          </div>          
          </div>
          <div className="form-group">
          <img src={values.imageSrc} width={250} height={250}  />
          </div>
          <br/> 
                     <div className="row">
                      <div className="form-group col-md-1">
                            <button style={{background: "green"}} type="submit">Save</button>
                            
                            </div>
                            
                     </div>                  

                    </form>
                    {/* <div className="form-group col-md-3">
                            <a href="/" className="float-right">
                              <button style={{background: "blue"}}>Back</button>
                            </a>                            
                            </div> */}
                    <div>

                    </div>
                </div>
            

    </div>
)
}

export default CreatePuppy;