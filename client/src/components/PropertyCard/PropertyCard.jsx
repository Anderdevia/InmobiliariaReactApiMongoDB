import React from "react";
import './PropertyCard.css'
import {truncate} from 'lodash'
import { useNavigate } from "react-router-dom";
const PropertyCard = ({card}) => {

  const navigate = useNavigate();

  let notFoundImage = "https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ=";
  let image = card?.images[0]?.file || notFoundImage;

  const formatCurrency = (amount) => {
    return new Intl.NumberFormat('en-US').format(amount);
  };

  return (
    <div className="property-card" onClick={() => navigate(`../properties/${card.idProperty}`)}>
      <div className="card-image-container">
        <img src={image} alt="home" className="card-image" />
      </div>
      <div className="card-content">
        <span className="card-price">
          <span style={{ color: "orange" }}>$</span>
          <span>{formatCurrency(card.price)}</span>
        </span>
        <span className="card-title">{truncate(card.name, { length: 30 })}</span>
        <span className="card-location">{card.address}</span> {/* Mostrar la dirección */}
      </div>
      <div className="card-footer">
        <span className="property-details">
          <span>{card.area} For sale</span>
          <span>{card.bedrooms} </span>
        </span>
      </div>
    </div>
  );
};

export default PropertyCard;
