import "../../styles.css";
import {CreatedServiceAtom} from "../../../atoms/atoms.ts";
import {useAtom} from "jotai";
export const TopBar = () => {
    const [createdService] = useAtom(CreatedServiceAtom);
    return (
        <div className="border-b px-4 mb-4 mt-2 pb-4 border-[--color-text-baby-blue]">
            <div className="flex items-center justify-between p-0.5">
                <div>
                    <span className="text-sm font-bold block">
                        Add available time slots
                    </span>
                    <span className="text-xs block">
                        Adding available time slots to {createdService?.name}
                    </span>
                </div>
            </div>
        </div>
    )
}
