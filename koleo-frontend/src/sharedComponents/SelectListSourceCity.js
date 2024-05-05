import * as React from 'react';
import { FormControl, InputLabel, MenuItem, Select } from '@mui/material';

const SelectLabels = ({ onCityChange, LabelName} ) => {
  const [city, setCity] = React.useState('');
  const dataArray = ['City 1', 'City 2', 'City 3'];

  const handleChange = (event) => {
    setCity(event.target.value);
    onCityChange(event.target.value);
  };

  return (
    <div>
      <FormControl sx={{ m: 1, minWidth: 120 }}>
        <InputLabel id="demo-simple-select-helper-label">{LabelName}</InputLabel>
        <Select
          labelId="demo-simple-select-helper-label"
          id="demo-simple-select-helper"
          value={city}
          label={LabelName}
          onChange={handleChange}
        >
        {dataArray.map((cityValue, index) => (
            <MenuItem key={index} value={cityValue}>
              {cityValue}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </div>
  );
}
export default SelectLabels;