import * as React from "react";
import Stack from "@mui/material/Stack";
import LinearProgress from "@mui/material/LinearProgress";

const CustomLinearProgress = () => {
  return (
    <Stack sx={{ width: "100%", color: "grey.500" }} spacing={2}>
      <LinearProgress color="secondary" />
      <LinearProgress color="success" />
    </Stack>
  );
};

export default CustomLinearProgress;
