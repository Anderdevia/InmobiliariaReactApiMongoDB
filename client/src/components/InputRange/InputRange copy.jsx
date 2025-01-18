import React from "react";
import { HiCurrencyDollar } from "react-icons/hi";
import "./InputRange.css"; // Asegúrate de importar el archivo CSS

const InputRange = ({ valueMin, setValueMin, valueMax, setValueMax }) => {
  const MIN_VALUE = 0; // $1,000,000
  const MID_VALUE2 = 1000000000;
  const MID_VALUE = 1000000;
  const MAX_VALUE = 1000000000; // $1,000,000,000

  // Formatear el valor a una moneda
  const formatCurrency = (amount) => {
    return new Intl.NumberFormat("en-US", {
      style: "currency",
      currency: "USD",
    }).format(amount);
  };

  // Manejadores del slider
  const handleSliderChange = (e) => {
    const newValue = e.target.value;
    if (e.target.name === "min") {
      setValueMin(MIN_VALUE + (MID_VALUE2 - MIN_VALUE) * (newValue / 100));
    } else if (e.target.name === "max") {
      setValueMax(MID_VALUE + (MAX_VALUE - MID_VALUE) * (newValue / 100));
    }
  };

  return (
    <div
      className="price_filter"
      style={{
        width: "30%",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <div
        className="price_slider_amount mb-20"
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          width: "100%",
        }}
      ></div>
      <div
        className="slider-range"
        style={{ width: "100%", marginTop: "15px", position: "relative" }}
      >
        {/* Slider de Rango de Precio */}
        <label
          style={{
            fontSize: "14px",
            fontWeight: "600",
            marginBottom: "10px",
            marginTop: "10px",
            display: "block",
          }}
        >
          {`Precio minimo ${formatCurrency(valueMin)}`}
        </label>

        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          {/* Primer slider */}
          <input
            type="range"
            name="min"
            min="0"
            max="100"
            value={((valueMin - MIN_VALUE) / (MID_VALUE - MIN_VALUE)) * 100}
            onChange={handleSliderChange}
            className="slider-input"
            style={{
              width: "45%",
              height: "6px",
              backgroundColor: "#e0e0e0",
              borderRadius: "5px",
            }}
          />
        </div>
      </div>


      <div
        className="slider-range"
        style={{ width: "100%", marginTop: "15px", position: "relative" }}
      >
        {/* Slider de Rango de Precio */}
        <label
          style={{
            fontSize: "14px",
            fontWeight: "600",
            marginBottom: "10px",
            marginTop: "10px",
            display: "block",
          }}
        >
          {`Precio maximo ${formatCurrency(valueMax)}`}
        </label>

        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          {/* Segundo slider */}
          <input
            type="range"
            name="max"
            min="0"
            max="100"
            value={((valueMax - MID_VALUE) / (MAX_VALUE - MID_VALUE)) * 100}
            onChange={handleSliderChange}
            className="slider-input"
            style={{
              width: "45%",
              height: "6px",
              backgroundColor: "#e0e0e0",
              borderRadius: "5px",
            }}
          />
        </div>
      </div>

    </div>
  );
};

export default InputRange;
