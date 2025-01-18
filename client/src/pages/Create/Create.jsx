import React, { useContext, useEffect, useState, useRef } from "react";
import { createImageProperty, createProperty  } from "../../utils/api.js";
import { PuffLoader } from "react-spinners";
// import "./Property.css";
import { TextInput, Box, Textarea, Group, Button, NumberInput } from "@mantine/core";
import { AiOutlineCloudUpload } from "react-icons/ai";
import { useForm } from "@mantine/form";
import { validateString } from "../../utils/common";
// import { validateString } from "../../utils/common";
import UserDetailContext from "../../context/UserDetailContext.js";
import { useNavigate } from "react-router-dom";
import './Create.css'
import { toast } from "react-toastify";

const Create = () => {

  const navigate = useNavigate();
  
  const [propertyDetails, setPropertyDetails] = useState({
    title: "",
    description: "",
    price: 0,
    address: "",
    years: 0,
    image: null,
  });

  const [imageURL, setImageURL] = useState(propertyDetails.image);
  const [ isLoading, setIsLoading ] = useState(false);

  const form = useForm({
    initialValues: {
      title: propertyDetails.title,
      address: propertyDetails.address,
      description: propertyDetails.description,
      price: propertyDetails.price,
      years: propertyDetails.years,
      image: propertyDetails.image,
    },
    validate: {
      title: (value) => validateString(value),
      description: (value) => validateString(value),
      address: (value) => validateString(value),
      image: (value) => value ? null : 'Debe subir una imagen',
      price: (value) =>
          value < 100000 ? "Debe ser mayor a 1.000.000 pesos" : null,
      years: (value) =>
        value < 1900 ? "Debe ser mayor a 1900 " : null,
    },
  });

  const {
    userDetails: { token, bookings },
    setUserDetails,
  } = useContext(UserDetailContext);

  if (isLoading) {
    return (
      <div className="wrapper">
        <div className="flexCenter paddings">
          <PuffLoader />
        </div>
      </div>
    );
  }

  const handleImageUpload = (event) => {
    const files = event.target.files;
    if (files) {
      for (let i = 0; i < files.length; i++) {
        const file = files[i];
        const reader = new FileReader();
        setImageURL(URL.createObjectURL(file));

        reader.onloadend = () => {
          const base64String = reader.result;
          //setImage(base64String);
          setPropertyDetails({ ...propertyDetails, image: base64String });
          form.setFieldValue("image", base64String);
        };
        // reader.result
        reader.readAsDataURL(file);
      }
    }
  }; 

  const handleSubmit = async ()=> {
    const {hasErrors} = form.validate()

    if(form.values.image === null) return toast.error("Debe subir una imagen");

    if(!hasErrors) { 

      setIsLoading(true); 

      let dataPost  ={
        idProperty: null,
        name: form.values.title,
        address: form.values.address,
        price: form.values.price,
        codeInternal: form.values.description,
        year: form.values.years,
        idOwner: "603f6e83f1d0f0a1e4b3a3e0"
      }

      let response = await createProperty(dataPost);

      if(response.idProperty) {
          let dataImage = {
            idPropertyImage: null,
            idProperty: response.idProperty,
            file:  form.values.image,
            enabled: true
          }
          let responseImage = await createImageProperty(dataImage);
          setIsLoading(false); 
          navigate(`../properties/${response.idProperty}`)
      }
    }
    else{
       toast.error("Error al guardar la propiedad");
    }
   }  

  return (
    <div className="wrapper">
      <div className="flexColStart paddings innerWidth property-container " style={{display: 'flex', alignItems: 'center'}}>
      
    
        <form  onSubmit={(e) => {
          e.preventDefault();
          handleSubmit();
        }}>

        <div className="flexCenter property-details">
          {/* left */}
          <div className="flexColStart left">
            {/* head */}
            <div className="flexStart head">
              <h1 className="orangeText">Crear propiedad</h1>           
            </div>


            <TextInput
              withAsterisk
              label="Nombre de la propiedad"
              placeholder="Nombre de la propiedad"
              {...form.getInputProps("title")}
              style={{width: '500px'}}
            />
            <Textarea
              placeholder="Descripción"
              label="Descripción"
              withAsterisk
              {...form.getInputProps("description")}
              style={{width: '500px'}}
            />

            <TextInput
              withAsterisk
              label="Dirección"
              placeholder="Calle 45..."
              {...form.getInputProps("address")}
              style={{width: '500px'}}
            />

            <NumberInput
              withAsterisk
              label="Precio"
              placeholder="10000000"
              min={0}
              {...form.getInputProps("price")}
              style={{width: '500px'}}
            />

            <NumberInput
              withAsterisk
              label="Año"
              placeholder="2024"
              min={0}
              max={2025}
              {...form.getInputProps("years")}
              style={{width: '500px'}}
            />
            
            <input
                    type="file"
                    id="file-input"
                    accept="image/*"
                    multiple
                    onChange={handleImageUpload}
                    style={{ display: 'none' }}
                />
                <label htmlFor="file-input" className="upload-box" style={{borderColor: 'gray', backgroundColor:'#f3f3f3'}}>
                    <AiOutlineCloudUpload size={50} color="grey" />  
                    <div className="upload-text" style={{color: 'gray', fontWeight:600 }}>Click para subir imagen</div>
                </label>

                {

                  form.values.image &&  <img src={imageURL} alt="home image" style={{maxWidth:500}} />
                }

                

            <button
              className="button"
              type="submit"
              // onClick={() => alert('Guardado')}
            >
              Guardar
            </button>
     
 
          </div>
 
        </div>

        </form>
      </div>
    </div>
  );
};

export default Create;
