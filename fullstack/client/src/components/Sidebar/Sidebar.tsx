import {AccountInfo} from "./AccountInfo.tsx";
import {Search} from "./Search.tsx";
import {RouteSelect} from "./RouteSelect.tsx";
import { Bottombar } from "./Bottombar.tsx";

const Sidebar = () => {
    return (
        <div>
            <div className="overflow-y-scroll sticky top-4 h-[calc(100vh-32px-48px)]">
                <AccountInfo/>
                <RouteSelect/>
            </div>
            <Bottombar/>
        </div>
    )
}

export default Sidebar