import React from 'react';

const TimeComponent = ({ time }) => {
  const [hour, minute] = time.split(':');

  const formattedTime = `${hour}:${minute}`;

  return (
    <div>{formattedTime}</div>
  );
};

export default TimeComponent;
