// src/components/TicketList.js
import React from 'react';
import AdvertismentItem from './AdvertismentItem';

const AdvertismentList = ({ ads }) => {
  return (
    <div className="ad-list">
      {ads.map((ad, index) => (
        <AdvertismentItem
            key={index} 
            AdContent={ad.adContent} 
            AdLinkUrl={ad.adLinkUrl}
            AdImageUrl={ad.adImageUrl}
            AdOwner={ad.adOwner} 
      />
      ))}
    </div>
  );
};

export default AdvertismentList;
