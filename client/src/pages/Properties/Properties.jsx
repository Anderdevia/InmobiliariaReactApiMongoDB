import React, { useEffect, useState } from "react";
import SearchBar from "../../components/SearchBar/SearchBar";
import InputRange from "../../components/InputRange/InputRange";

import "./Properties.css";
import useProperties from "../../hooks/useProperties";
import { PuffLoader } from "react-spinners";
import PropertyCard from "../../components/PropertyCard/PropertyCard";
import { useLocation } from "react-router-dom";
import { set } from "lodash";

const Properties = () => {

  const location = useLocation();

  const { dataProperties, isError, isLoading, images } = useProperties();
  const [filter, setFilter] = useState("");
  const [valueMin, setValueMin] = useState("");
  const [valueMax, setValueMax] = useState("");
  const [ dataFiltered, setDataFiltered ] = useState(dataProperties);

  useEffect(() => { 

    const queryParams = new URLSearchParams(location.search);
    const filterWord = queryParams.get("filter");
    if (filterWord && dataProperties.length > 0) {
      setFilter(filterWord);
      setTimeout(() => handleFilter(handleFilter()), 1000);       
    }
    setDataFiltered(dataProperties);
  }, [dataProperties])

  if (isError) {
    return (
      <div className="wrapper">
        <span>Error while fetching data</span>
      </div>
    );
  }

  const handleFilter = () => {
    console.log('Filtering by --->',filter);
    let newData = dataProperties
    .filter(
      (property) =>
        property?.name?.toLowerCase().includes(filter.toLowerCase()) ||
        property?.address?.toLowerCase().includes(filter.toLowerCase())
    );
    newData = newData.filter(
      (property) =>
        (valueMin === '' || property?.price >= parseFloat(valueMin)) &&
        (valueMax === '' || property?.price <= parseFloat(valueMax))
    );
    setDataFiltered(newData)
  }

  if (isLoading) {
    return (
      <div className="wrapper flexCenter" style={{ height: "60vh" }}>
        <PuffLoader
          height="80"
          width="80"
          radius={1}
          color="#4066ff"
          aria-label="puff-loading"
        />
      </div>
    );
  }

  return (
    <div className="wrapper">
      <div className="flexColCenter paddings innerWidth properties-container">

        <div className="flexCenter search-bar" style={{width: '100%'}}>
          <SearchBar filter={filter} setFilter={setFilter} />
          <InputRange valueMax={valueMax} valueMin={valueMin} setValueMax={setValueMax} setValueMin={setValueMin}/>
          <button className="button" onClick={()=>handleFilter()}>Buscar</button>
        </div>


        <div className="paddings flexCenter properties">
          {

            dataFiltered
              .map((card, i) => (
                <PropertyCard card={card} key={i} />
              ))
          }
        </div>
      </div>
    </div>
  );
};

export default Properties;
