import { useEffect, useState } from "react";
import { useParams,useNavigate } from "react-router-dom";
import http from "../http-common";

// call two methods one is Get by Id
// second method is for updating a contact. PUT method

const EditPuppy = () =>{
    const { contactid } = useParams();

    useEffect(() => {
      //api get by id endpoint url   
      fetch("https://localhost:7154/api/Puppy/" + contactid).then((res) => {
            return res.json();
        }).then((resp) => {          
          nameChange(resp.name)
          breedChange(resp.breed)
          birthDateChange(resp.birthDate)
          imageChange(resp.image)
          imageNameChange(resp.imageName)
          imageSrcChange(resp.imageSrc)
         
        }).catch((err) => {
            console.log(err.message);
        })
    }, [contactid]);


    const [name,nameChange]= useState("");
    const [breed,breedChange]= useState("");
    const [birthDate,birthDateChange]= useState("");  
    const [image,imageChange]= useState<string|File>("");
    const [imageName,imageNameChange]= useState("");
    const [imageSrc,imageSrcChange]= useState("/img/image_placeholder.png");
    const navigate=useNavigate();
    const handelsubmit = (e: any) =>{
      e.preventDefault();
      const formData = new FormData()
      formData.append('name', name)
      formData.append('breed', breed)
      formData.append('birthDate', birthDate)
      formData.append('image', image)
      http.put("/api/Puppy/"+contactid, formData, {
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
    function showPreview(e: any)  {
      if (e.target.files && e.target.files[0]) {
          let image = e.target.files[0];
          const reader = new FileReader();
          reader.onload = (ev: any) => {
           
            imageChange(                 
                  image
                   )
                   imageSrcChange(reader.result as string)
          }
          
          reader.readAsDataURL(image)
      }
        
    }

    return(
        <div className="container">
        <br/>
                <div>
                    <div>
                        <h2>Update puppy information</h2>
                    </div>
                    <br/>
                    <form onSubmit={handelsubmit} className="div-border">
                    <div className="row">
                        <div className="form-group col-md-1">
                        <label>*Name</label>
                        </div>
                        <div className="col-md-2">
                        <input value={name} onChange={e=>nameChange(e.target.value)}></input>
                        </div>
                     </div>
                     <div className="row">
                        <div className="form-group col-md-1">
                        <label>Breed</label>
                        </div>
                        <div className="col-md-2">
                        <input value={breed} onChange={e=>breedChange(e.target.value)}></input>
                        </div>
                        </div>
                        <div className="row">
                        <div className="form-group col-md-1">
                        <label>*BirthDate</label>
                        </div>
                        <div className="col-md-2">
                        <input value={birthDate} onChange={e=>birthDateChange(e.target.value)}></input>
                        </div>
                        </div>
                        <br/>
                        <br/>
                        <div className="row">
                        <div className="form-group col-md-1">
                        <label>Image </label>
                        
                        </div>
                                             
                        
          <div className="col-md-2">
          <input  type="file" id="image"  accept="image/*" onChange={showPreview} />      
          </div>          
          </div>
          <div className="form-group">
          <img  src={imageSrc} width={250} height={250}  />
          </div>
          <br/> 
                      

                        <div className="row">
                      <div className="form-group col-md-1">
                            <button style={{background: "green"}} type="submit">Save</button>
                            
                            </div>
                            {/* <div className="form-group col-md-3">
                            <a href="/" className="float-right">
                              <button style={{background: "blue"}}>Back</button>
                            </a>                            
                            </div> */}
                     </div>                      
                    </form>
                    <div>

                    </div>
                </div>  
    </div>
    )
    }
    
    export default EditPuppy;