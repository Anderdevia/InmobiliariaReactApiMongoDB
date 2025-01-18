import React from "react";
import { HiLocationMarker } from "react-icons/hi";
import { useNavigate } from "react-router-dom";

const SearchBar = ({ filter = "", setFilter = ()=>{}}) => {

  const navigate = useNavigate();

  const [filterInput, setFilterInput] = React.useState(filter);

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      const formattedFilter = filterInput.replace(/\s+/g, "-");
      navigate(`../properties?filter=${formattedFilter}`);
    }
  }; 
  
  return (
    <div className="search-bar">
      <HiLocationMarker color="var(--blue)" size={25} />
      <input
        placeholder="Buscar por nombre o dirección..."
        type="text"
        value={filterInput}
        style={{width: '80%'}}
        onChange={(e) => { setFilterInput(e.target.value); setFilter(e.target.value); }}
        onKeyDown={handleKeyDown}

      />
    </div>
  );
};

export default SearchBar;
