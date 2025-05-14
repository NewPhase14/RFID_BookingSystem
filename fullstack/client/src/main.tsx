import {createRoot} from 'react-dom/client'
import App from "./components/App.tsx";
import {Toaster} from "react-hot-toast";
import {BrowserRouter} from "react-router";

createRoot(document.getElementById('root')!).render(
  <BrowserRouter>
          <App />
      <Toaster />
  </BrowserRouter>,
)
