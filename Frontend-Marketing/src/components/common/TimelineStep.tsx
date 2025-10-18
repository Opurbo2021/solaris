import type { ReactNode } from 'react';

interface TimelineStepProps {
  icon: ReactNode;
  title: string;
  description: string;
  position: 'left' | 'right';
}

export default function TimelineStep({ icon, title, description, position }: TimelineStepProps) {
  return (
    <div className={`flex items-center gap-8 ${position === 'left' ? 'flex-row' : 'flex-row-reverse'}`}>
      {/* Content Side */}
      <div className={`flex-1 ${position === 'left' ? 'text-right' : 'text-left'}`}>
        <div className="inline-block">
          <div className="flex items-center gap-4">
            {position === 'left' && (
              <>
                <div>
                  <h3 className="text-xl font-bold text-white mb-1">{title}</h3>
                  <p className="text-sm text-gray-400">{description}</p>
                </div>
                <div className="w-14 h-14 rounded-full bg-primary flex items-center justify-center flex-shrink-0">
                  <div className="text-black text-2xl">{icon}</div>
                </div>
              </>
            )}
            {position === 'right' && (
              <>
                <div className="w-14 h-14 rounded-full bg-primary flex items-center justify-center flex-shrink-0">
                  <div className="text-black text-2xl">{icon}</div>
                </div>
                <div>
                  <h3 className="text-xl font-bold text-white mb-1">{title}</h3>
                  <p className="text-sm text-gray-400">{description}</p>
                </div>
              </>
            )}
          </div>
        </div>
      </div>

      {/* Timeline Dot */}
      <div className="w-4 h-4 rounded-full bg-white border-4 border-primary flex-shrink-0 z-10" />

      {/* Empty Space on Other Side */}
      <div className="flex-1" />
    </div>
  );
}