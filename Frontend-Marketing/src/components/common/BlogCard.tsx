import { HiArrowRight } from 'react-icons/hi2';

interface BlogCardProps {
  image: string;
  title: string;
  excerpt: string;
  slug: string;
}

export default function BlogCard({ image, title, excerpt, slug }: BlogCardProps) {
  return (
    <div className="bg-white/5 backdrop-blur-sm rounded-xl border border-white/10 overflow-hidden hover:border-primary/30 transition-all hover:shadow-lg hover:shadow-primary/10 flex flex-col">
      {/* Blog Image */}
      <div className="relative h-56 overflow-hidden">
        <img 
          src={image} 
          alt={title}
          className="w-full h-full object-cover"
        />
      </div>
      
      {/* Card Content */}
      <div className="p-6 flex flex-col flex-grow">
        <h3 className="text-xl font-bold text-white mb-3 leading-tight">
          {title}
        </h3>
        <p className="text-gray-400 text-sm leading-relaxed mb-6 flex-grow">
          {excerpt}
        </p>
        
        {/* Read More Link */}
        <a 
          href={`/blog/${slug}`}
          className="inline-flex items-center gap-2 text-primary font-semibold hover:gap-3 transition-all group"
        >
          Read More
          <HiArrowRight className="w-5 h-5 group-hover:translate-x-1 transition-transform" />
        </a>
      </div>
    </div>
  );
}