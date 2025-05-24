import RegisterView from "./RegisterView.tsx";
import {TopBar} from "./TopBar.tsx";

export const RegisterUser = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4">
            <TopBar/>
            <RegisterView/>
        </div>
    )
}
