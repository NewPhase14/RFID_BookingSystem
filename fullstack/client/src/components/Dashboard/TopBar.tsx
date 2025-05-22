import "../styles.css";

export const TopBar = () => {
    return (
        <div className="border-b px-4 mb-4 mt-2 pb-4 border-[--color-text-baby-blue]">
            <div className="flex items-center justify-between p-0.5">
                <div>
                    <span className="text-sm font-bold block">
                    Welcome back, {localStorage.getItem("firstname")}
                    </span>
                    <span className="text-xs block">
                    {Date().split("G")[0]}
                    </span>
                </div>
            </div>
        </div>
    );
};

