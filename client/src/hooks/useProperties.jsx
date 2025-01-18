import React, { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { getAllProperties, getAllImages } from "../utils/api";

const useProperties = () => {

  let data = [];
  let isError = false;
  let refetch = null;

  const [ dataProperties, setDataProperties ] = useState([])
  const [ isLoading, setLoading ] = useState(true);

  let [ images, setImages ] = useState([])

  useEffect(() => {
    setLoading(true);
    getAllProperties().then((res) => {
      console.log('Properties --->',res);
      loadImages(res);
    });
  }, []);

  let loadImages = (data) => {

    getAllImages().then((res) => {
      setImages(res);

      data?.map((property) => {
        let resImage = res.filter((img) => img.idProperty === property.idProperty);
        property.images = resImage;
      });
      setDataProperties(data);
      setTimeout(() => setLoading(false), 1000);
      //setLoading(false);
    });

  }

  return {
    data,
    dataProperties,
    images,
    isError,
    isLoading,
    refetch,
  };
};

export default useProperties;
